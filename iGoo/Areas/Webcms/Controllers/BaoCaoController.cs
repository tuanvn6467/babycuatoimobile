using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using iGoo.Areas.Webcms.Models;
using iGoo.Helpers;
using System.Data;
using iGoo.Classes;

using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using ClosedXML.Excel;
using System.IO;

namespace iGoo.Areas.Webcms.Controllers
{
    public class BaoCaoController : DefaultController
    {
        private String per = Libs.GetPermission("ORDER");

        private List<DataRow> list;

        BaoCaoViewModel iv = new BaoCaoViewModel();
        UserViewModel sv = new UserViewModel();
        OrderViewModel ov = new OrderViewModel();
        CategoryViewModel ct = new CategoryViewModel();

        public ActionResult Index()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");

            //BaoCaoViewModel iv = new BaoCaoViewModel();
            //UserViewModel sv = new UserViewModel();
            //OrderViewModel ov = new OrderViewModel();
            //CategoryViewModel ct = new CategoryViewModel();

            ViewBag.NVBH = sv.SelectUserOptimize().AsEnumerable().ToList();


            ViewBag.CusClass = ov.SelectMenuCusClass().AsEnumerable().ToList();


            ct.MenuAll = ct.SelectOptimize();
            ViewBag.MenuCate = ct.SelectMenu(new Guid("87A2AADF-D5F7-455D-AF80-71ECC2B683AC"));

            if (!Request.IsNull("txtKey"))
                iv.Title = Request.Get("txtKey");
            if (!Request.IsNull("txtFromDate"))
                iv.FromDate = Request.Get("txtFromDate");
            else
                iv.FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            if (!Request.IsNull("txtToDate"))
                iv.ToDate = Request.Get("txtToDate");
            if (!Request.IsNull("slSearchCate"))
                iv.CategoryID = new Guid(Request.Get("slSearchCate"));
            if (!Request.IsNull("slsUser"))
                iv.NVBH = new Guid(Request.Get("slsUser"));
            if (!Request.IsNull("cboCusClass"))
                iv.CusClassID = new Guid(Request.Get("cboCusClass"));

            //iv.ShipperID = Request.IsNull("ShipperID") ? iv.ShipperID = Guid.Empty : iv.ShipperID = new Guid(Request.Get("ShipperID"));

            iv.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            iv.PageSize = Request.IsNull("show") ? 20 : Request.GetNumber("show");

            if (!Request.IsNull("OrderBy") && !Request.IsNull("Order"))
                iv.OrderBy = Request.Get("OrderBy") + " " + Request.Get("Order");

            //Page
            //List<DataRow> list = iv.ReportDoanhThu().AsEnumerable().ToList();
            list = iv.ReportDoanhThu_BySP().AsEnumerable().ToList();
            ViewBag.BaoCao = list;
            ViewBag.TotalRows = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"])) : 0;
            ViewBag.TotalPages = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"]) / (double)iv.PageSize) : 0;
            //Sum            
            if (list.Count > 0)
            {
                ViewBag.Totals = iv.Report_DoanhThu_BySP_Tong().AsEnumerable().ToList();
            }
            return View();
        }

        [HttpPost]
        public void ExportData()
        {
            try
            {
                if (!Request.IsNull("txtKey"))
                    iv.Title = Request.Get("txtKey");
                if (!Request.IsNull("txtFromDate"))
                    iv.FromDate = Request.Get("txtFromDate");
                else
                    iv.FromDate = DateTime.Now.ToString("dd/MM/yyyy");
                if (!Request.IsNull("txtToDate"))
                    iv.ToDate = Request.Get("txtToDate");
                if (!Request.IsNull("slSearchCate"))
                    iv.CategoryID = new Guid(Request.Get("slSearchCate"));
                if (!Request.IsNull("slsUser"))
                    iv.NVBH = new Guid(Request.Get("slsUser"));
                if (!Request.IsNull("cboCusClass"))
                    iv.CusClassID = new Guid(Request.Get("cboCusClass"));

                //iv.ShipperID = Request.IsNull("ShipperID") ? iv.ShipperID = Guid.Empty : iv.ShipperID = new Guid(Request.Get("ShipperID"));

                iv.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
                iv.PageSize = Request.IsNull("show") ? 999999 : Request.GetNumber("show");

                if (!Request.IsNull("OrderBy") && !Request.IsNull("Order"))
                    iv.OrderBy = Request.Get("OrderBy") + " " + Request.Get("Order");

                //Page
                //List<DataRow> list = iv.ReportDoanhThu().AsEnumerable().ToList();
                list = iv.ReportDoanhThu_BySP().AsEnumerable().ToList();
                List<BaoCaoDoanhThu_BySP_Export> ReportList = new List<BaoCaoDoanhThu_BySP_Export>();
                int stt = 0;
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    stt++;
                    //BaoCaoDoanhThu_Export id = new BaoCaoDoanhThu_Export();
                    BaoCaoDoanhThu_BySP_Export id = new BaoCaoDoanhThu_BySP_Export();
                    id.STT = stt.ToString();
                    id.MaSanPham = list[i]["SKU"].ToString();
                    id.SoLuong = Convert.ToInt32(list[i]["soluong"].ToString());
                    id.TongTien = Convert.ToDouble(list[i]["total"].ToString());

                    ReportList.Add(id);

                }

                // install closedXML(third-party) tool to your project and  add namespace " using ClosedXML.Excel;".
                XLWorkbook wb = new XLWorkbook();
                string sheetName = "BaoCaoDoanhThu"; //Give name for export file. 
                var ws = wb.Worksheets.Add(sheetName);
                ws.Cell(1, 1).InsertTable(ReportList);  // assign list here.
                HttpContext.Response.Clear();
                HttpContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Response.AddHeader("content-disposition", String.Format(@"attachment;filename={0}.xlsx", sheetName.Replace(" ", "_")));

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.WriteTo(HttpContext.Response.OutputStream);
                    memoryStream.Close();
                }

                HttpContext.Response.End();
                //string returnUrl = Request.Get("returnUrl");
                //return Redirect("/Webcms/InventoryDetail" + returnUrl + "&result=1");

            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                Redirect("/Webcms/BaoCao?error=1");
            }
            //return View("MyView"); 

        }
    }
}




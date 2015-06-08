using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iGoo.Areas.Webcms.Models;
using iGoo.Helpers;
using System.Diagnostics;
using System.Data;
using iGoo.Classes;
using System.Text;

using ClosedXML.Excel;
using System.IO;

namespace iGoo.Areas.Webcms.Controllers
{
    public class InventoryLogController : DefaultController
    {
        //
        // GET: /Webcms/InventoryLog/
        private String per = Libs.GetPermission("INVENTORYLOG");


        public ActionResult Index()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            //Select Inventory

            try
            {

            InventoryViewModel iv = new InventoryViewModel();
            List<DataRow> listIv = iv.SelectOptimize().AsEnumerable().ToList();
            ViewBag.Inventory = listIv;

            // add form
            //Select menu
            CategoryViewModel ct = new CategoryViewModel();
            //ct.Code = "CATEGORY_PRODUCT";
            //ViewBag.GroupProduct = ct.SelectChild().AsEnumerable().ToList();
            ct.MenuAll = ct.SelectOptimize();
            ViewBag.MenuCate = ct.SelectMenu(new Guid("87A2AADF-D5F7-455D-AF80-71ECC2B683AC"));

            AttributeViewModel at = new AttributeViewModel();
            at.Code = "ATTRIBUTE_MANUFACTURE";
            ViewBag.ManuFacture = at.SelectChild().AsEnumerable().ToList();
                
                ////Select User
            UserViewModel uv = new UserViewModel();
            List<DataRow> ListUv = uv.SelectUserOptimize().AsEnumerable().ToList();
            ViewBag.User = ListUv;


            InventoryLogViewModel idv = new InventoryLogViewModel();

            //if (!Request.IsNull("txtKey"))
            //    idv.ProductName = Request.Get("txtKey");
            if (!Request.IsNull("txtKey"))
                idv.SoChungTu = Request.Get("txtKey");
            if (!Request.IsNull("slManuFacture"))
                idv.ChungLoai = new Guid(Request.Get("slManuFacture"));
            if (!Request.IsNull("slSearchChangeType"))
                idv.ChangeType = Request.GetNumber("slSearchChangeType");
            //if (!Request.IsNull("slSearchCate"))
            //    idv.CategoryID = new Guid(Request.Get("slSearchCate"));

            if (!Request.IsNull("slSearchInventory"))
                idv.InventoryID = new Guid(Request.Get("slSearchInventory"));

            if (!Request.IsNull("slUser"))
                idv.UserID = new Guid(Request.Get("slUser"));

            //IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);

            if (!Request.IsNull("txtStartDate"))
                idv.StartDate =  Request.Get("txtStartDate");
                //idv.StartDate =  DateTime.Parse(Request.Get("txtStartDate"),culture);
            if (!Request.IsNull("txtEndDate"))
                idv.EndDate = Request.Get("txtEndDate");
                //idv.EndDate = DateTime.Parse(Request.Get("txtEndDate"), culture);            
                
            idv.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            idv.PageSize = Request.IsNull("show") ? 20 : Request.GetNumber("show");

            if (!Request.IsNull("OrderBy") && !Request.IsNull("Order"))
                idv.OrderBy = Request.Get("OrderBy") + " " + Request.Get("Order");


            //Page
            List<DataRow> list = idv.SelectAll().AsEnumerable().ToList();
            ViewBag.InventoryLog = list;
            ViewBag.TotalPages = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"]) / (double)idv.PageSize) : 0;

            return View();
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/InventoryLog?error=1");
            }
        }

        [HttpPost]
        public void ExportData()
        {
            try
            {
                InventoryLogViewModel idv = new InventoryLogViewModel();

                //if (!Request.IsNull("txtKey"))
                //    idv.ProductName = Request.Get("txtKey");
                if (!Request.IsNull("txtKey"))
                    idv.SoChungTu = Request.Get("txtKey");
                if (!Request.IsNull("slManuFacture"))
                    idv.ChungLoai = new Guid(Request.Get("slManuFacture"));
                if (!Request.IsNull("slSearchChangeType"))
                    idv.ChangeType = Request.GetNumber("slSearchChangeType");
                //if (!Request.IsNull("slSearchCate"))
                //    idv.CategoryID = new Guid(Request.Get("slSearchCate"));

                if (!Request.IsNull("slSearchInventory"))
                    idv.InventoryID = new Guid(Request.Get("slSearchInventory"));

                if (!Request.IsNull("slUser"))
                    idv.UserID = new Guid(Request.Get("slUser"));

                //IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);

                if (!Request.IsNull("txtStartDate"))
                    idv.StartDate = Request.Get("txtStartDate");
                //idv.StartDate =  DateTime.Parse(Request.Get("txtStartDate"),culture);
                if (!Request.IsNull("txtEndDate"))
                    idv.EndDate = Request.Get("txtEndDate");
                //idv.EndDate = DateTime.Parse(Request.Get("txtEndDate"), culture);    

                //Page
                //List<DataRow> list = iv.ReportDoanhThu().AsEnumerable().ToList();
                List<DataRow> list = idv.SelectAll_Export().AsEnumerable().ToList();
                List<InventoryLogs_SelectAll_Export> ReportList = new List<InventoryLogs_SelectAll_Export>();
                int stt = 0;
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    stt++;
                    InventoryLogs_SelectAll_Export id = new InventoryLogs_SelectAll_Export();
                    id.STT = stt.ToString();
                    id.NgayThucHien = list[i]["ModifyDate"].ToString();
                    id.MaSP = list[i]["SKU"].ToString();
                    id.TenSP = list[i]["Title"].ToString();
                    id.HinhThuc = list[i]["HinhThuc"].ToString();
                    id.KhoXuat = list[i]["KhoXuat"].ToString();
                    id.KhoNhan = list[i]["KhoNhap"].ToString();
                    id.SoLuong = Convert.ToInt32(list[i]["QuantityChange"].ToString());
                    id.TonDau = Convert.ToInt32(list[i]["TonDau"].ToString());
                    id.TonCuoi = Convert.ToInt32(list[i]["TonCuoi"].ToString());
                    id.TinhTrang = list[i]["TinhTrang"].ToString();
                    id.ChungTu = list[i]["SoChungTu"].ToString();
                    id.NguoiThucHien = list[i]["FullName"].ToString();

                    ReportList.Add(id);

                }

                // install closedXML(third-party) tool to your project and  add namespace " using ClosedXML.Excel;".
                XLWorkbook wb = new XLWorkbook();
                string sheetName = "LichSuGiaoDichKho"; //Give name for export file. 
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

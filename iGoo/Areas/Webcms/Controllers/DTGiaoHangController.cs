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
    public class DTGiaoHangController : DefaultController
    {
        private String per = Libs.GetPermission("ORDER");

        private List<DataRow> list;

        DTGiaoHangViewModel iv = new DTGiaoHangViewModel();
        UserViewModel sv = new UserViewModel();
        OrderViewModel ov = new OrderViewModel();
        
        public ActionResult Index()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");

            //Slect group
            CategoryViewModel ct = new CategoryViewModel();
            ShipperViewModel sv = new ShipperViewModel();
            
            ct.MenuAll = ct.SelectOptimize();
            ViewBag.MenuCate = ct.SelectMenu(new Guid("87A2AADF-D5F7-455D-AF80-71ECC2B683AC"));

            AttributeViewModel at = new AttributeViewModel();
            at.Code = "ATTRIBUTE_MANUFACTURE";
            ViewBag.ManuFacture = at.SelectChild().AsEnumerable().ToList();
            at.Code = "ATTRIBUTE_PRODUCT_STATUS";
            ViewBag.Type = at.SelectChild().AsEnumerable().ToList();
            at.Code = "ATTRIBUTE_PRODUCT";
            ViewBag.Filter = at.SelectChild().AsEnumerable().ToList();

            ViewBag.NVGH = sv.SelectOptimize().AsEnumerable().ToList();            

            ViewBag.CusClass = ov.SelectMenuCusClass().AsEnumerable().ToList();
            InventoryViewModel inv = new InventoryViewModel();
            List<DataRow> listIv = inv.SelectOptimize().AsEnumerable().ToList();
            ViewBag.Inventory = listIv;

            ct.MenuAll = ct.SelectOptimize();
            ViewBag.MenuCate = ct.SelectMenu(new Guid("87A2AADF-D5F7-455D-AF80-71ECC2B683AC"));

            if (!Request.IsNull("txtKey"))
                iv.Title = Request.Get("txtKey");

            if (!Request.IsNull("slManuFacture"))
                iv.ChungLoai = new Guid(Request.Get("slManuFacture"));
            if (!Request.IsNull("txtHinhThucBan"))
                iv.TypeBuy = Request.GetNumber("txtHinhThucBan");
            if (!Request.IsNull("slsUser"))
                iv.NVGH = new Guid(Request.Get("slsUser"));
            if (!Request.IsNull("cboCusClass"))
                iv.CusClassID = new Guid(Request.Get("cboCusClass"));            

            if (!Request.IsNull("txtFromDate"))
                iv.FromDate = Request.Get("txtFromDate");
            //else
            //    iv.FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            if (!Request.IsNull("txtToDate"))
                iv.ToDate = Request.Get("txtToDate");
            if (!Request.IsNull("slSearchInventory"))
                iv.InventoryID = new Guid(Request.Get("slSearchInventory"));      

            list = iv.ReportDoanhThu_GiaoHang().AsEnumerable().ToList();            
            ViewBag.BaoCao = list;               

            return View();
        }

        [HttpPost]
        public void ExportData()
        {
            try
            {
                if (!Request.IsNull("txtKey"))
                    iv.Title = Request.Get("txtKey");

                if (!Request.IsNull("slManuFacture"))
                    iv.ChungLoai = new Guid(Request.Get("slManuFacture"));
                if (!Request.IsNull("txtHinhThucBan"))
                    iv.TypeBuy = Request.GetNumber("txtHinhThucBan");
                if (!Request.IsNull("slsUser"))
                    iv.NVGH = new Guid(Request.Get("slsUser"));
                if (!Request.IsNull("cboCusClass"))
                    iv.CusClassID = new Guid(Request.Get("cboCusClass"));

                if (!Request.IsNull("txtFromDate"))
                    iv.FromDate = Request.Get("txtFromDate");
                //else
                //    iv.FromDate = DateTime.Now.ToString("dd/MM/yyyy");
                if (!Request.IsNull("txtToDate"))
                    iv.ToDate = Request.Get("txtToDate");                 

                //Page
                //List<DataRow> list = iv.ReportDoanhThu().AsEnumerable().ToList();
                list = iv.ReportDoanhThu_GiaoHang().AsEnumerable().ToList();
                List<BaoCaoDoanhThu_ByNVBH_Export> ReportList = new List<BaoCaoDoanhThu_ByNVBH_Export>();
                int stt = 0;
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    stt++;                    
                    BaoCaoDoanhThu_ByNVBH_Export id = new BaoCaoDoanhThu_ByNVBH_Export();
                    id.STT = stt.ToString();
                    id.TenNV = list[i]["FullName"].ToString();
                    id.OLBuon = Convert.ToInt32(list[i]["OLBuon"].ToString());
                    id.SRBuon = Convert.ToInt32(list[i]["SRBuon"].ToString());
                    id.DTOLBuon = Convert.ToDouble(list[i]["DTOLBuon"].ToString());
                    id.DTSRBuon = Convert.ToDouble(list[i]["DTSRBuon"].ToString());
                    id.OLLe = Convert.ToInt32(list[i]["OLLe"].ToString());
                    id.SRLe = Convert.ToInt32(list[i]["SRLe"].ToString());
                    id.DTOLLe = Convert.ToDouble(list[i]["DTOLLe"].ToString());
                    id.DTSRLe = Convert.ToDouble(list[i]["DTSRLe"].ToString());                     

                    ReportList.Add(id);

                }

                // install closedXML(third-party) tool to your project and  add namespace " using ClosedXML.Excel;".
                XLWorkbook wb = new XLWorkbook();
                string sheetName = "BaoCaoDoanhThu_GH"; //Give name for export file. 
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




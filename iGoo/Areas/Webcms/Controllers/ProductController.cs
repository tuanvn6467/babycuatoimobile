using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using iGoo.Areas.Webcms.Models;
using iGoo.Helpers;
using System.Data;
using iGoo.Classes;
using ClosedXML.Excel;

namespace iGoo.Areas.Webcms.Controllers
{
    public class ProductController : DefaultController
    {
        private String per = Libs.GetPermission("PRODUCT");
        public ActionResult Index()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            //Select group
            CategoryViewModel ct = new CategoryViewModel();
            //ct.Code = "CATEGORY_PRODUCT";
            //ViewBag.GroupProduct = ct.SelectChild().AsEnumerable().ToList();
            ct.MenuAll = ct.SelectOptimize();
            ViewBag.MenuCate = ct.SelectMenu(new Guid("87A2AADF-D5F7-455D-AF80-71ECC2B683AC"));

            //select inventory
            InventoryViewModel inv = new InventoryViewModel();
            ViewBag.MenuInv = inv.SelectMenu().AsEnumerable().ToList();

            AttributeViewModel at = new AttributeViewModel();
            at.Code = "ATTRIBUTE_PRODUCT_STATUS";
            ViewBag.GroupType = at.SelectChild().AsEnumerable().ToList();

            //Select news
            ProductViewModel pv = new ProductViewModel();
            if (!Request.IsNull("txtKey"))
                pv.Title = Request.Get("txtKey");
            if (!Request.IsNull("slSearchCate"))
                pv.CategoryID = new Guid(Request.Get("slSearchCate"));
            if (!Request.IsNull("slSearchType"))
                pv.Type = Request.Get("slSearchType");
            if (!Request.IsNull("slSearchStatus"))
                pv.Status = Request.GetNumber("slSearchStatus");
            if (!Request.IsNull("slSearchInv"))
                pv.InventoryID = new Guid(Request.Get("slSearchInv"));
            else
                pv.InventoryID = new Guid("0c80dcd0-5d8e-4041-acaf-2cf7c2916162");

            pv.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            pv.PageSize = Request.IsNull("show") ? 20 : Request.GetNumber("show");


            if (!Request.IsNull("OrderBy") && !Request.IsNull("Order"))
                pv.OrderBy = Request.Get("OrderBy") + " " + Request.Get("Order");

            //Eidt
            if (!Request.IsNull("ID"))
            {
                pv.ProductID = new Guid(Request.Get("ID"));
                ViewBag.Edit = pv.SelectOne().AsEnumerable().ToList();
            }

            //Page
            List<DataRow> list = pv.SelectAll().AsEnumerable().ToList();
            ViewBag.Product = list;
            ViewBag.TotalPages = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"]) / (double)pv.PageSize) : 0;
            return View();
        }

        //ProductSearch sonln added 2013oct18 
        public ActionResult ProductSearch()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            //Select group
            CategoryViewModel ct = new CategoryViewModel();
            //ct.Code = "CATEGORY_PRODUCT";
            //ViewBag.GroupProduct = ct.SelectChild().AsEnumerable().ToList();
            ct.MenuAll = ct.SelectOptimize();
            ViewBag.MenuCate = ct.SelectMenu(new Guid("87A2AADF-D5F7-455D-AF80-71ECC2B683AC"));

            AttributeViewModel at = new AttributeViewModel();
            at.Code = "ATTRIBUTE_PRODUCT_STATUS";
            ViewBag.GroupType = at.SelectChild().AsEnumerable().ToList();

            //select inventory
            InventoryViewModel inv = new InventoryViewModel();
            ViewBag.MenuInv = inv.SelectMenu().AsEnumerable().ToList();

            //Select news
            ProductViewModel pv = new ProductViewModel();
            int cusStatus = 1;
            if (!Request.IsNull("txtKey"))
                pv.Title = Request.Get("txtKey");
            if (!Request.IsNull("slSearchCate"))
                pv.CategoryID = new Guid(Request.Get("slSearchCate"));
            if (!Request.IsNull("slSearchType"))
                pv.Type = Request.Get("slSearchType");
            if (!Request.IsNull("slSearchStatus"))
                pv.Status = Request.GetNumber("slSearchStatus");
            if (!Request.IsNull("slSearchInv"))
                pv.InventoryID = new Guid(Request.Get("slSearchInv"));
            else
                pv.InventoryID = new Guid("0c80dcd0-5d8e-4041-acaf-2cf7c2916162");
            if (!Request.IsNull("slSearchCus"))
                cusStatus = Request.GetNumber("slSearchCus");
            //if (pv.InventoryID.Equals("0c80dcd0-5d8e-4041-acaf-2cf7c2916162"))
            if (pv.InventoryID == new Guid("0c80dcd0-5d8e-4041-acaf-2cf7c2916162"))
            {
                if (cusStatus == 1)
                {
                    pv.CustomerType = 1;
                }
                else
                {
                    pv.CustomerType = 2;
                }
            }
            else if (pv.InventoryID == new Guid("6197d743-0b16-4bcf-8048-3803ae3fdcd8"))
            {
                if (cusStatus == 1)
                {
                    pv.CustomerType = 3;
                }
                else
                {
                    pv.CustomerType = 4;
                }
            }


            pv.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            pv.PageSize = Request.IsNull("show") ? 20 : Request.GetNumber("show");


            if (!Request.IsNull("OrderBy") && !Request.IsNull("Order"))
                pv.OrderBy = Request.Get("OrderBy") + " " + Request.Get("Order");

            //Eidt
            if (!Request.IsNull("ID"))
            {
                pv.ProductID = new Guid(Request.Get("ID"));
                ViewBag.Edit = pv.SelectOne().AsEnumerable().ToList();
            }

            //Page
            List<DataRow> list = pv.SelectAll1().AsEnumerable().ToList();
            ViewBag.Product = list;
            ViewBag.TotalPages = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"]) / (double)pv.PageSize) : 0;
            return View();
        }//abc

        public ActionResult ProductSearchBarcode()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            List<DataRow> list = new List<DataRow>();
            String cookies = "";
            if (HttpContext.Request.Cookies["barcode_search"] == null)
            {
                Libs.WriteCookie("barcode_search", "");
            }
            else
            {
                cookies = Libs.ReadCookie("barcode_search");
                if (!String.IsNullOrEmpty(cookies))
                {
                    String[] row = Regex.Split(cookies, "\\|\\|");
                    for (int i = 0; i < row.Length; i++)
                    {
                        ProductViewModel pv1 = new ProductViewModel();
                        pv1.Title = row[i];
                        pv1.PageIndex = 1;
                        pv1.PageSize = 20;
                        if (!Request.IsNull("slSearchInv"))
                            pv1.InventoryID = new Guid(Request.Get("slSearchInv"));
                        else
                            pv1.InventoryID = new Guid("665f2362-fe8a-4169-9513-0109340f3c0b");
                        int cusStatus1 = 1;
                        if (!Request.IsNull("slSearchCus"))
                            cusStatus1 = Request.GetNumber("slSearchCus");
                        if (pv1.InventoryID == new Guid("0c80dcd0-5d8e-4041-acaf-2cf7c2916162"))
                        {
                            if (cusStatus1 == 1)
                            {
                                pv1.CustomerType = 1;
                            }
                            else
                            {
                                pv1.CustomerType = 2;
                            }
                        }
                        else if (pv1.InventoryID == new Guid("6197d743-0b16-4bcf-8048-3803ae3fdcd8"))
                        {
                            if (cusStatus1 == 1)
                            {
                                pv1.CustomerType = 3;
                            }
                            else
                            {
                                pv1.CustomerType = 4;
                            }
                        }
                        List<DataRow> li = pv1.SelectFromBarcode().AsEnumerable().ToList();
                        list.AddRange(li);
                    }
                }
            }
            //Select group
            CategoryViewModel ct = new CategoryViewModel();
            //ct.Code = "CATEGORY_PRODUCT";
            //ViewBag.GroupProduct = ct.SelectChild().AsEnumerable().ToList();
            ct.MenuAll = ct.SelectOptimize();
            ViewBag.MenuCate = ct.SelectMenu(new Guid("87A2AADF-D5F7-455D-AF80-71ECC2B683AC"));
            //select inventory
            InventoryViewModel inv = new InventoryViewModel();
            ViewBag.MenuInv = inv.SelectMenu().AsEnumerable().ToList();

            AttributeViewModel at = new AttributeViewModel();
            at.Code = "ATTRIBUTE_PRODUCT_STATUS";
            ViewBag.GroupType = at.SelectChild().AsEnumerable().ToList();

            //Select news
            ProductViewModel pv = new ProductViewModel();
            int cusStatus = 1;
            if (!Request.IsNull("txtKey"))
                pv.Title = Request.Get("txtKey");
            if (!Request.IsNull("slSearchCate"))
                pv.CategoryID = new Guid(Request.Get("slSearchCate"));
            if (!Request.IsNull("slSearchType"))
                pv.Type = Request.Get("slSearchType");
            if (!Request.IsNull("slSearchStatus"))
                pv.Status = Request.GetNumber("slSearchStatus");
            if (!Request.IsNull("slSearchInv"))
                pv.InventoryID = new Guid(Request.Get("slSearchInv"));
            else
                pv.InventoryID = new Guid("665f2362-fe8a-4169-9513-0109340f3c0b");
            if (!Request.IsNull("slSearchCus"))
                cusStatus = Request.GetNumber("slSearchCus");
            //if (pv.InventoryID.Equals("0c80dcd0-5d8e-4041-acaf-2cf7c2916162"))
            if (pv.InventoryID == new Guid("0c80dcd0-5d8e-4041-acaf-2cf7c2916162"))
            {
                if (cusStatus == 1)
                {
                    pv.CustomerType = 1;
                }
                else
                {
                    pv.CustomerType = 2;
                }
            }
            else if (pv.InventoryID == new Guid("6197d743-0b16-4bcf-8048-3803ae3fdcd8"))
            {
                if (cusStatus == 1)
                {
                    pv.CustomerType = 3;
                }
                else
                {
                    pv.CustomerType = 4;
                }
            }

            pv.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            pv.PageSize = Request.IsNull("show") ? 20 : Request.GetNumber("show");


            if (!Request.IsNull("OrderBy") && !Request.IsNull("Order"))
                pv.OrderBy = Request.Get("OrderBy") + " " + Request.Get("Order");
            if (pv.Title.ToString().Length == 0)
            {
                pv.Title = new SqlString("----------------");
            }
            else
            {
                pv.Title = new SqlString(pv.Title.ToString());
            }
            List<DataRow> l = pv.SelectFromBarcode().AsEnumerable().ToList();
            list.AddRange(l);
            if (l.Count > 0)
            {
                if (cookies.Length > 0)
                {
                    cookies = string.Concat(cookies, "||", pv.Title);
                }
                else
                {
                    cookies = string.Concat(pv.Title);
                }
                Libs.WriteCookie("barcode_search", cookies);
            }

            ViewBag.Product = list;
            return View();
        }

        // sonln end

        public ActionResult Add()
        {
            LoadDefault();

            if (per.IndexOf("I") < 0)
                return View("NotPermission");
            //Slect group
            CategoryViewModel ct = new CategoryViewModel();
            //ct.Code = "CATEGORY_PRODUCT";
            //ViewBag.GroupProduct = ct.SelectChild().AsEnumerable().ToList();
            ct.MenuAll = ct.SelectOptimize();
            ViewBag.MenuCate = ct.SelectMenu(new Guid("87A2AADF-D5F7-455D-AF80-71ECC2B683AC"));

            AttributeViewModel at = new AttributeViewModel();
            at.Code = "ATTRIBUTE_MANUFACTURE";
            ViewBag.ManuFacture = at.SelectChild().AsEnumerable().ToList();
            at.Code = "ATTRIBUTE_PRODUCT_STATUS";
            ViewBag.Type = at.SelectChild().AsEnumerable().ToList();
            at.Code = "ATTRIBUTE_PRODUCT";
            ViewBag.Filter = at.SelectChild().AsEnumerable().ToList();

            //Select user
            ProductViewModel pv = new ProductViewModel();

            //Eidt
            if (!Request.IsNull("ID"))
            {
                pv.ProductID = new Guid(Request.Get("ID"));
                ViewBag.Edit = pv.SelectOne().AsEnumerable().ToList();

            }

            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create()
        {
            try
            {
                CheckUser();
                ProductViewModel pv = new ProductViewModel();
                pv.CategoryID = new Guid(Request.Get("slSearchCate"));
                pv.UserID = new Guid(Session["UserID"].ToString());
                pv.ManufactureID = new Guid(Request.Get("slManuFacture"));
                pv.Title = Request.Get("txtTitle");
                pv.SEOName = Request.Get("txtSEOName");
                pv.Image = Request.Get("txtImage");
                pv.Brief = Request.Get("txtBrief");
                pv.Content = Request.Get("txtContent");
                pv.MetaTitle = Request.Get("txtMetaTitle");
                pv.MetaKeyword = Request.Get("txtMetaKeyword");
                pv.MetaDescription = Request.Get("txtMetaDescription");
                pv.Tags = Libs.ReplaceSpace("  ", " ", Request.Get("txtTags"));
                pv.TagsTitle = Libs.ReplaceSpace("  ", " ", Request.Get("txtTagsTitle"));
                pv.Related = Request.Get("txtRelated");
                pv.Type = Request.Get("ckType").Replace(',', '#');
                pv.Status = Request.GetNumber("slStatus");
                pv.SlideImage = Request.Get("txtSlideImage");
                pv.SKU = Request.Get("txtSKU");
                pv.Quantity = Request.GetNumber("txtQuantity");
                pv.ImportPrice = Request.GetDecimal("txtImportPrice");
                pv.RealPrice = Request.GetDecimal("txtRealPrice");
                pv.SalePrice = Request.GetDecimal("txtSalePrice");
                //sln added b
                pv.SalePriceDealer = Request.GetDecimal("txtSalePriceDealer");
                pv.SalePriceHCM = Request.GetDecimal("txtSalePriceHCM");
                pv.SalePriceDealerHCM = Request.GetDecimal("txtSalePriceDealerHCM");
                pv.SalePriceCN3 = Request.GetDecimal("txtSalePriceCN3");
                pv.SalePriceDealerCN3 = Request.GetDecimal("txtSalePriceDealerCN3");
                pv.ProductBarcode = Request.Get("txtBarcode");
                //sln added b
                pv.DiscountPercent = Request.GetNumber("txtDiscountPercent");
                pv.Model = Request.Get("txtModel");
                pv.Filter = Request.Get("ckFilter").Replace(',', '#');
                pv.ProductAttribute = Request.Get("txtProductAttribute");
                pv.TransportFee = Request.Get("txtTransportFee");
                pv.Promotion = Request.Get("txtPromotion");
                pv.Parameter = Request.Get("txtParameter");
                if (!Request.IsNull("txtOrder"))
                    pv.Order = Request.GetNumber("txtOrder");
                else
                    pv.Order = 999;
                if (!Request.IsNull("txtPollID"))
                    pv.PollID = new Guid(Request.Get("txtPollID"));
                if (Request.IsNull("ID"))
                {
                    if (per.IndexOf("I") < 0)
                        return View("NotPermission");
                    pv.Visitor = 0;
                    pv.TotalComment = 0;
                    pv.Created = DateTime.Now;
                    pv.ProductID = Guid.NewGuid();
                    pv.Insert();
                }
                else
                {
                    if (per.IndexOf("U") < 0)
                        return View("NotPermission");
                    if (!Request.IsNull("ckRefresh"))
                        pv.Created = DateTime.Now;
                    pv.ProductID = new Guid(Request.Get("ID"));
                    pv.Update();
                }

                return Redirect("/Webcms/Product/Add?ID=" + Request.Get("ID") + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Product/Add?ID=" + Request.Get("ID") + "&error=1");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update()
        {
            try
            {
                if (per.IndexOf("U") < 0)
                    return View("NotPermission");
                ProductViewModel pv = new ProductViewModel();
                for (int i = 1; i <= Request.GetNumber("count"); i++)
                {
                    if (!Request.IsNull("ckID-" + i.ToString()))
                    {
                        pv.ProductID = new Guid(Request.Get("ckID-" + i.ToString()));
                        pv.Status = (Request.GetNumber("slStatus-" + i.ToString()));
                        if (!Request.IsNull("txtOrder-" + i.ToString()))
                            pv.Order = (Request.GetNumber("txtOrder-" + i.ToString()));
                        else
                            pv.Order = 999;
                        if (!Request.IsNull("txtsaleprice-" + i.ToString()))
                            pv.SalePrice = Request.GetDecimal("txtsaleprice-" + i.ToString());
                        else
                            pv.SalePrice = 0;
                        if (!Request.IsNull("txtsalepricedealer-" + i.ToString()))
                            pv.SalePriceDealer = Request.GetDecimal("txtsalepricedealer-" + i.ToString());
                        else
                            pv.SalePriceDealer = 0;
                        if (!Request.IsNull("txtsalepricehcm-" + i.ToString()))
                            pv.SalePriceHCM = Request.GetDecimal("txtsalepricehcm-" + i.ToString());
                        else
                            pv.SalePriceHCM = 0;
                        if (!Request.IsNull("txtsalepricedealerhcm-" + i.ToString()))
                            pv.SalePriceDealerHCM = Request.GetDecimal("txtsalepricedealerhcm-" + i.ToString());
                        else
                            pv.SalePriceDealerHCM = 0;
                        if (!Request.IsNull("txtsalepricecn3-" + i.ToString()))
                            pv.SalePriceCN3 = Request.GetDecimal("txtsalepricecn3-" + i.ToString());
                        else
                            pv.SalePriceCN3 = 0;
                        if (!Request.IsNull("txtsalepricedealercn3-" + i.ToString()))
                            pv.SalePriceDealerCN3 = Request.GetDecimal("txtsalepricedealercn3-" + i.ToString());
                        else
                            pv.SalePriceDealerCN3 = 0;
                        pv.Update();
                    }
                }

                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/Product" + returnUrl + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Product?error=1");
            }
        }

        [HttpPost]
        public ActionResult Delete()
        {
            try
            {
                if (per.IndexOf("D") < 0)
                    return View("NotPermission");
                ProductViewModel pv = new ProductViewModel();
                for (int i = 1; i <= Request.GetNumber("count"); i++)
                {
                    if (!Request.IsNull("ckID-" + i.ToString()))
                    {
                        pv.ProductID = new Guid(Request.Get("ckID-" + i.ToString()));
                        pv.Delete();
                    }
                }

                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/Product" + returnUrl + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Product?error=1");
            }
        }

        public ActionResult ExportExcel()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            //Slect group
            CategoryViewModel ct = new CategoryViewModel();
            //ct.Code = "CATEGORY_PRODUCT";
            //ViewBag.GroupProduct = ct.SelectChild().AsEnumerable().ToList();
            ct.MenuAll = ct.SelectOptimize();
            ViewBag.MenuCate = ct.SelectMenu(new Guid("87A2AADF-D5F7-455D-AF80-71ECC2B683AC"));

            AttributeViewModel at = new AttributeViewModel();
            at.Code = "ATTRIBUTE_PRODUCT_STATUS";
            ViewBag.GroupType = at.SelectChild().AsEnumerable().ToList();

            //select inventory
            InventoryViewModel inv = new InventoryViewModel();
            ViewBag.MenuInv = inv.SelectMenu().AsEnumerable().ToList();

            //Select news
            ProductViewModel pv = new ProductViewModel();
            if (!Request.IsNull("txtKey"))
                pv.Title = Request.Get("txtKey");
            if (!Request.IsNull("slSearchCate"))
                pv.CategoryID = new Guid(Request.Get("slSearchCate"));
            if (!Request.IsNull("slSearchType"))
                pv.Type = Request.Get("slSearchType");
            if (!Request.IsNull("slSearchStatus"))
                pv.Status = Request.GetNumber("slSearchStatus");
            if (!Request.IsNull("slSearchInv"))
                pv.InventoryID = new Guid(Request.Get("slSearchInv"));
            else
                pv.InventoryID = new Guid("665f2362-fe8a-4169-9513-0109340f3c0b");

            pv.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            pv.PageSize = Request.IsNull("show") ? 20000 : Request.GetNumber("show");


            if (!Request.IsNull("OrderBy") && !Request.IsNull("Order"))
                pv.OrderBy = Request.Get("OrderBy") + " " + Request.Get("Order");

            //Page
            List<DataRow> list = pv.SelectAll2().AsEnumerable().ToList();
            ViewBag.Product = list;
            ViewBag.TotalPages = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"]) / (double)pv.PageSize) : 0;
            return View();
        }

        [HttpPost]
        public void ExportExcelFromDatabase()
        {
            //SAVE THE DATA IN LIST             
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<ProductImportModel> productList = new List<ProductImportModel>();
            for (int i = 1; i <= Request.GetNumber("count"); i++)
            {
                if (!Request.IsNull("ckID-" + i.ToString()))
                {
                    ProductImportModel pi = new ProductImportModel();
                    pi.MaSP = Request.Get("ckID-" + i.ToString());
                    //pi.ProductName = Request.Get("txtTitle-" + i.ToString());
                    pi.GiaLeHN = Request.Get("txtSalePrice-" + i.ToString());
                    pi.GiaLeHN = pi.GiaLeHN.Replace(",", "");
                    pi.GiaBuonHN = Request.Get("txtSalePriceDealer-" + i.ToString());
                    pi.GiaBuonHN = pi.GiaBuonHN.Replace(",", "");
                    pi.GiaLeHCM = Request.Get("txtSalePriceHCM-" + i.ToString());
                    pi.GiaLeHCM = pi.GiaLeHCM.Replace(",", "");
                    pi.GiaBuonHCM = Request.Get("txtSalePriceDealerHCM-" + i.ToString());
                    pi.GiaBuonHCM = pi.GiaBuonHCM.Replace(",", "");
                    pi.GiaLeCN3 = Request.Get("txtSalePriceCN3-" + i.ToString());
                    pi.GiaLeCN3 = pi.GiaLeCN3.Replace(",", "");
                    pi.GiaBuonCN3 = Request.Get("txtSalePriceDealerCN3-" + i.ToString());
                    pi.GiaBuonCN3 = pi.GiaBuonCN3.Replace(",", "");
                    productList.Add(pi);
                }
            }

            // install closedXML(third-party) tool to your project and  add namespace " using ClosedXML.Excel;".
            XLWorkbook wb = new XLWorkbook();
            string sheetName = "ProductList"; //Give name for export file. 
            var ws = wb.Worksheets.Add(sheetName);
            ws.Cell(1, 1).InsertTable(productList);  // assign list here.
            HttpContext.Response.Clear();
            HttpContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Response.AddHeader("content-disposition", String.Format(@"attachment;filename={0}.xlsx", sheetName.Replace(" ", "_")));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(HttpContext.Response.OutputStream);
                memoryStream.Close();
            }

            HttpContext.Response.End(); ;

        }
        public List<ProductImportModel> getExportProductList()
        {
            List<ProductImportModel> lpi = new List<ProductImportModel>();
            for (int i = 1; i <= Request.GetNumber("count"); i++)
            {
                ProductImportModel pi = new ProductImportModel();
                if (!Request.IsNull("ckID-" + i.ToString()))
                {
                    //pi.ProductId = Request.Get("ckID-" + i.ToString());
                    //pi.ProductName = Request.Get("txtTitle-" + i.ToString());
                    //pi.PriceHN = Request.Get("txtSalePrice-" + i.ToString());
                    //pi.PriceDealerHN = Request.Get("txtSalePriceDealer-" + i.ToString());
                    //pi.PriceHCM = Request.Get("txtSalePriceHCM-" + i.ToString());
                    //pi.PriceDealerHCM = Request.Get("txtSalePriceDealerHCM-" + i.ToString());

                    pi.MaSP = Request.Get("ckID-" + i.ToString());
                    //pi.ProductName = Request.Get("txtTitle-" + i.ToString());
                    pi.GiaLeHN = Request.Get("txtSalePrice-" + i.ToString());
                    pi.GiaBuonHN = Request.Get("txtSalePriceDealer-" + i.ToString());
                    pi.GiaLeHCM = Request.Get("txtSalePriceHCM-" + i.ToString());
                    pi.GiaBuonHCM = Request.Get("txtSalePriceDealerHCM-" + i.ToString());
                    pi.GiaLeCN3 = Request.Get("txtSalePriceCN3-" + i.ToString());
                    pi.GiaBuonCN3 = Request.Get("txtSalePriceDealerCN3-" + i.ToString());
                }
                lpi.Add(pi);
            }
            return lpi;
        }

        public ActionResult ImportExcel()
        {
            LoadDefault();
            return View();
        }

        //public ActionResult DownloadFile()
        //{
        //    string sPath = "../FileMau/DanhSachSanPham.xls";
        //    //sPath.Replace("\\", "/");
        //    string name = Path.GetFileName(sPath);
        //    return View();
        //}

        [HttpPost]
        public ActionResult ImportExcel(HttpPostedFileBase excelFile)
        {
            LoadDefault();
            try
            {
                var connectionString = "";
                InventoryViewModel iv = new InventoryViewModel();
                List<DataRow> listIv = iv.SelectOptimize().AsEnumerable().ToList();
                ViewBag.Inventory = listIv;

                if (excelFile != null)
                {

                    //Save the uploaded file to the disc.
                    string savedFileName = "~/Uploads/" + excelFile.FileName;
                    excelFile.SaveAs(Server.MapPath(savedFileName));
                    //Create a connection string to access the Excel file using the ACE provider.

                    string fileExt = System.IO.Path.GetExtension(Server.MapPath(savedFileName));
                    //This is for excel 2003
                    if (fileExt.Equals(".xls"))
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties= \"Excel 8.0;IMEX=1;\"", Server.MapPath(savedFileName));
                    //This is for Excel 2007. 
                    if (fileExt.Equals(".xlsx"))
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties= \"Excel 12.0;IMEX=1;\"", Server.MapPath(savedFileName));


                    ViewBag.FilePath = Server.MapPath(savedFileName);

                    //Fill the dataset with information from the  worksheet.
                    var adapter = new OleDbDataAdapter("SELECT * FROM [ProductList$]", connectionString);
                    var ds = new DataSet();
                    adapter.Fill(ds, "results");
                    DataTable data = ds.Tables["results"];

                    List<DataRow> listPreview = data.AsEnumerable().ToList();
                    //ViewBag.File = listPreview;
                    string err = "";
                    int c = 0, hn = 0, hnd = 0, hcm = 0, hcmd = 0, cn3 = 0, cn3d = 0;
                    if (!Request.IsNull("chkHN"))
                        hn = 1;
                    if (!Request.IsNull("chkHN1"))
                        hnd = 1;
                    if (!Request.IsNull("chkHCM"))
                        hcm = 1;
                    if (!Request.IsNull("chkHCM1"))
                        hcmd = 1;
                    if (!Request.IsNull("chkCN3"))
                        cn3 = 1;
                    if (!Request.IsNull("chkCN31"))
                        cn3d = 1;
                    foreach (DataRow dr in listPreview)
                    {
                        if (dr["MaSP"].ToString().Length > 0)
                        {
                            ProductViewModel pv = new ProductViewModel();
                            pv.SKU = dr["MaSP"].ToString();
                            if (pv.CheckProductCode())
                            {
                                //Gia HN
                                if (dr["GiaLeHN"].ToString().Length > 0)
                                    pv.SalePrice = SqlDecimal.Parse(dr["GiaLeHN"].ToString());
                                else
                                    pv.SalePrice = 0;
                                if (dr["GiaBuonHN"].ToString().Length > 0)
                                    pv.SalePriceDealer = SqlDecimal.Parse(dr["GiaBuonHN"].ToString());
                                else
                                    pv.SalePriceDealer = 0;
                                //Gia HCM
                                if (dr["GiaLeHCM"].ToString().Length > 0)
                                    pv.SalePriceHCM = SqlDecimal.Parse(dr["GiaLeHCM"].ToString());
                                else
                                    pv.SalePriceHCM = 0;
                                if (dr["GiaBuonHCM"].ToString().Length > 0)
                                    pv.SalePriceDealerHCM = SqlDecimal.Parse(dr["GiaBuonHCM"].ToString());
                                else
                                    pv.SalePriceDealerHCM = 0;
                                //Gia CN3
                                if (dr["GiaLeCN3"].ToString().Length > 0)
                                    pv.SalePriceCN3 = SqlDecimal.Parse(dr["GiaLeCN3"].ToString());
                                else
                                    pv.SalePriceCN3 = 0;
                                if (dr["GiaBuonCN3"].ToString().Length > 0)
                                    pv.SalePriceDealerCN3 = SqlDecimal.Parse(dr["GiaBuonCN3"].ToString());
                                else
                                    pv.SalePriceDealerCN3 = 0;

                                pv.HN = hn;
                                pv.HN1 = hnd;
                                pv.HCM = hcm;
                                pv.HCM1 = hcmd;
                                pv.CN3 = cn3;
                                pv.CN31 = cn3d;

                                pv.UpdatePrice();
                                c++;
                            }
                            else
                            {
                                err += dr["MaSP"].ToString() + ",";
                            }
                        }
                        else
                        {
                            err += dr["MaSP"].ToString() + ",";
                        }
                    }
                    if (err.Length > 0)
                    {
                        if(c==0)
                            return Redirect("/Webcms/Product/ImportExcel?prer=1&err=" + err);
                        else
                            return Redirect("/Webcms/Product/ImportExcel?success="+c+"&prer=1&err=" + err);
                    }
                    else
                    {
                        return Redirect("/Webcms/Product/ImportExcel?success="+c);
                    }
                }
                else
                {
                    if (!Request.IsNull("txtFilePath"))
                    {
                        ViewBag.FilePath = Request.Get("txtFilePath");
                        string fileExt = System.IO.Path.GetExtension(Request.Get("txtFilePath"));
                        //This is for excel 2003
                        if (fileExt.Equals(".xls"))
                            connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties= \"Excel 8.0;IMEX=1;\"", Request.Get("txtFilePath"));
                        //This is for Excel 2007. 
                        if (fileExt.Equals(".xlsx"))
                            connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties= \"Excel 12.0;IMEX=1;\"", Request.Get("txtFilePath"));

                        //Fill the dataset with information from the  worksheet.
                        var adapter = new OleDbDataAdapter("SELECT * FROM [ProductList$$]", connectionString);
                        var ds = new DataSet();
                        adapter.Fill(ds, "results");
                        DataTable data = ds.Tables["results"];

                        List<DataRow> listPreview = data.AsEnumerable().ToList();

                        string err = "";
                        int c = 0;
                        foreach (DataRow dr in listPreview)
                        {
                            if (dr["MaSP"].ToString().Length > 0)
                            {
                                ProductViewModel pv = new ProductViewModel();
                                pv.SKU = dr["MaSP"].ToString();
                                if (pv.CheckProductCode())
                                {
                                    //Gia HN
                                    if (dr["GiaLeHN"].ToString().Length > 0)
                                        pv.SalePrice = SqlDecimal.Parse(dr["GiaLeHN"].ToString());
                                    else
                                        pv.SalePrice = 0;
                                    if (dr["GiaBuonHN"].ToString().Length > 0)
                                        pv.SalePriceDealer = SqlDecimal.Parse(dr["GiaBuonHN"].ToString());
                                    else
                                        pv.SalePriceDealer = 0;
                                    //Gia HCM
                                    if (dr["GiaLeHCM"].ToString().Length > 0)
                                        pv.SalePriceHCM = SqlDecimal.Parse(dr["GiaLeHCM"].ToString());
                                    else
                                        pv.SalePriceHCM = 0;
                                    if (dr["GiaBuonHCM"].ToString().Length > 0)
                                        pv.SalePriceDealerHCM = SqlDecimal.Parse(dr["GiaBuonHCM"].ToString());
                                    else
                                        pv.SalePriceDealerHCM = 0;
                                    //Gia CN3
                                    if (dr["GiaLeCN3"].ToString().Length > 0)
                                        pv.SalePriceCN3 = SqlDecimal.Parse(dr["GiaLeCN3"].ToString());
                                    else
                                        pv.SalePriceCN3 = 0;
                                    if (dr["GiaBuonCN3"].ToString().Length > 0)
                                        pv.SalePriceDealerCN3 = SqlDecimal.Parse(dr["GiaBuonCN3"].ToString());
                                    else
                                        pv.SalePriceDealerCN3 = 0;
                                    //action
                                    pv.UpdatePrice();
                                }
                                else
                                {
                                    err += dr["MaSP"].ToString() + ",";
                                }
                            }
                            else
                            {
                                err += dr["MaSP"].ToString() + ",";
                            }
                        }
                        if (err.Length > 0)
                        {
                            if (c == 0)
                                return Redirect("/Webcms/Product/ImportExcel?prer=1&err=" + err);
                            else
                                return Redirect("/Webcms/Product/ImportExcel?success=" + c + "&prer=1&err=" + err);
                        }
                        else
                        {
                            return Redirect("/Webcms/Product/ImportExcel?success=" + c);
                        }
                    }
                    else
                        return Redirect("/Webcms/Product/ImportExcel?error=1");
                }
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Product/ImportExcel?error=1");
            }
            //return View();
        }

        public string CheckProduct(string invId, string orderCode) //JsonResult
        {
            string resultStr = "1";
            ProductViewModel pv = new ProductViewModel();
            pv.Title = orderCode;
            pv.InventoryID = new SqlGuid(invId);
            List<DataRow> list = pv.CheckProductInOrder().AsEnumerable().ToList();
            foreach (DataRow dataRow in list)
            {
                if (Int16.Parse(dataRow["Quantity"].ToString()) <= 0 || Int16.Parse(dataRow["Quantity"].ToString()) < Int16.Parse(dataRow["Quantity1"].ToString()))
                    resultStr = "0";
            }
            return resultStr;
        }

        public string CheckSKU(string sSKU) //JsonResult
        {
            string resultStr = "0";
            ProductViewModel pv = new ProductViewModel();
            pv.SKU = sSKU;
            List<DataRow> list = pv.CheckProductSKU().AsEnumerable().ToList();
            if(list.Count > 0)
            {
                resultStr = "1";
            }
            return resultStr;
        }

        public string GetProductByCode(string pCode, string invId)
        {
//            string resultStr = "";
            ProductViewModel pv = new ProductViewModel();
            pv.Title = pCode;
            pv.InventoryID = new SqlGuid(invId);
            //List<DataRow> list = pv.GetSuggestProduct().AsEnumerable().ToList();
            DataTable dt = pv.GetSuggestProduct();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        public string GetProductByCode1(string pCode, string invId)
        {
            string resultStr = "";
            ProductViewModel pv = new ProductViewModel();
            pv.Title = pCode;
            pv.InventoryID = new SqlGuid(invId);
            List<DataRow> list = pv.GetSuggestProduct().AsEnumerable().ToList();
            foreach (DataRow dataRow in list)
            {
                                resultStr += "<a href=\"#\"><div class=\"show\" align=\"left\">";
                                resultStr += "<img src=\"/thumb/" + Libs.ThumbName(dataRow["Image"].ToString(), "50x50") + "\" style=\"width:50px; height:50px; float:left; margin-right:6px;\">";
                                resultStr += "<span class=\"name\"><strong>" + dataRow["Title"] + "</strong></span><br/>";
                                resultStr += "Giá: <strong>" + dataRow["SalePrice"] + "</strong></div></a>";
            }
            return resultStr;
        }

        public string GetProductByQuantity(string pId, string invId)
        {
            ProductViewModel pv = new ProductViewModel();
            pv.ProductID = new SqlGuid(pId);
            pv.InventoryID = new SqlGuid(invId);
            DataTable dt = pv.CheckProductQuantity();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
    }
}

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
using System.Data.OleDb;
using System.Data.SqlTypes;

namespace iGoo.Areas.Webcms.Controllers
{
    public class InventoryDetailController : DefaultController
    {
        //
        // GET: /Webcms/InventoryDetail/
        private String per = Libs.GetPermission("INVENTORYDETAIL");
        private string alert = "";

        public ActionResult Index()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            //Select Inventory
           
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

            InventoryDetailViewModel idv = new InventoryDetailViewModel();

            if (!Request.IsNull("txtKey"))
                idv.ProductName = Request.Get("txtKey");
            if (!Request.IsNull("slSearchStatus"))
                idv.Status = Request.GetNumber("slSearchStatus");
            if (!Request.IsNull("slSearchCate"))
                idv.CategoryID = new Guid(Request.Get("slSearchCate"));

            idv.InventoryID = Request.IsNull("slSearchType") ? idv.InventoryID = new Guid(listIv[0]["InventoryID"].ToString()) : idv.InventoryID = new Guid(Request.Get("slSearchType"));
            idv.InventoryDetailID = Request.IsNull("InventoryDetailID") ? idv.InventoryDetailID = Guid.Empty : idv.InventoryDetailID = new Guid(Request.Get("InventoryDetailID"));



            idv.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            idv.PageSize = Request.IsNull("show") ? 20 : Request.GetNumber("show");

            if (!Request.IsNull("OrderBy") && !Request.IsNull("Order"))
                idv.OrderBy = Request.Get("OrderBy") + " " + Request.Get("Order");


            //Page
            List<DataRow> list = idv.SelectAll().AsEnumerable().ToList();
            ViewBag.InventoryDetail = list;
            ViewBag.TotalPages = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"]) / (double)idv.PageSize) : 0;

            return View();
        }


        //Page
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update()
        {
            try
            {
                if (per.IndexOf("U") < 0)
                    return View("NotPermission");
                InventoryDetailViewModel idv = new InventoryDetailViewModel();
                for (int i = 1; i <= Request.GetNumber("count"); i++)
                {
                    if (!Request.IsNull("ckID-" + i.ToString()))
                    {
                        idv.ProductID = new Guid(Request.Get("ckID-" + i.ToString()));
                        idv.ChangeType = (Request.GetNumber("slSChangeType-" + i.ToString()));
                        idv.UserID = new Guid(Session["UserID"].ToString());

                        if (!Request.IsNull("txtQuantityChange-" + i.ToString()))
                            idv.QuantityChange = (Request.GetNumber("txtQuantityChange-" + i.ToString()));
                        else
                            idv.QuantityChange = 0;

                        if (!Request.IsNull("txtBrokenQuantityChange-" + i.ToString()))
                            idv.BrokenQuantityChange = (Request.GetNumber("txtBrokenQuantityChange-" + i.ToString()));
                        else
                            idv.BrokenQuantityChange = 0;

                        idv.InventoryID = new Guid(Request.Get("hdfInventoryID-" + i.ToString()));

                        idv.Description = "Thay đổi số lượng kho bởi chức năng quản lý kho";


                        if (!Request.IsNull("hdfInventoryDetailID-" + i.ToString()))
                        {
                            
                                idv.InventoryDetailID = new Guid(Request.Get("hdfInventoryDetailID-" + i.ToString()));
                                if (idv.ChangeType == 0)
                                {
                                    if (!Request.IsNull("hdfQuantity-" + i.ToString()))
                                        idv.Quantity = (Request.GetNumber("hdfQuantity-" + i.ToString()));
                                    else
                                        idv.Quantity = 0;

                                    if (!Request.IsNull("hdfBrokenQuantity-" + i.ToString()))
                                        idv.BrokenQuantity = (Request.GetNumber("hdfBrokenQuantity-" + i.ToString()));
                                    else
                                        idv.BrokenQuantity = 0;

                                    if (idv.BrokenQuantity < idv.BrokenQuantityChange || idv.Quantity < idv.QuantityChange)
                                    {
                                        Libs.WriteError("ChangeTypeWrong " + idv.ChangeType.ToString());
                                        return Redirect("/Webcms/InventoryDetail?error=1");
                                    }
                                }
                                idv.Change();
                            
                                
                        }
                        else
                        {
                            if (idv.ChangeType == 0)
                            {
                                Libs.WriteError("ChangeTypeWrong " + idv.ChangeType.ToString());
                                return Redirect("/Webcms/InventoryDetail?error=1");
                            }
                            else
                            {
                                CheckUser();
                                idv.InventoryDetailID = Guid.NewGuid();
                                idv.Quantity = idv.QuantityChange;
                                idv.BrokenQuantity = idv.BrokenQuantityChange;

                                if (per.IndexOf("I") < 0)
                                    return View("NotPermission");
                                idv.Insert();
                            }
                        }
                    }
                }
                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/InventoryDetail" + returnUrl + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/InventoryDetail?error=1");
            }
        }


        public ActionResult ImportProduct()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            //Slect group
            
            //select inventory                        
            InventoryViewModel iv = new InventoryViewModel();
            string strUserId = (string)Session["UserID"];
            //select inventory            
            iv.UserID = new SqlGuid(strUserId);
            List<DataRow> listIv = iv.SelectUserMenu().AsEnumerable().ToList();
            ViewBag.MenuInv = listIv;

            List<DataRow> listPreview = new List<DataRow>();
            ViewBag.File = listPreview;
            return View();
        }

        [HttpPost]
        public ActionResult ImportProduct(HttpPostedFileBase excelFile)
        {
            try
            {
                var connectionString = "";
                //select inventory                        
                InventoryViewModel iv = new InventoryViewModel();
                string strUserId = (string)Session["UserID"];
                //select inventory            
                iv.UserID = new SqlGuid(strUserId);
                List<DataRow> listIv = iv.SelectUserMenu().AsEnumerable().ToList();
                ViewBag.MenuInv = listIv;

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
                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    adapter.Fill(ds, "results");
                    DataTable data = ds.Tables["results"];

                    List<DataRow> listPreview = data.AsEnumerable().ToList();
                    ViewBag.File = listPreview;
                    
                    return View();
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
                        var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                        var ds = new DataSet();
                        adapter.Fill(ds, "results");
                        DataTable data = ds.Tables["results"];

                        List<DataRow> listPreview = data.AsEnumerable().ToList();
                        ViewBag.File = listPreview;
                        return View();
                    }
                    else
                        return Redirect("/Webcms/InventoryDetail/ImportProduct?error=1");
                }
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/InventoryDetail/ImportProduct?error=1");
            }
            //return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ImportUpdate()
        {
            try
            {
                
                if (per.IndexOf("U") < 0)
                    return View("NotPermission");
                InventoryDetailViewModel idv = new InventoryDetailViewModel();
                InventoryViewModel iv = new InventoryViewModel();
                List<DataRow> listIv = iv.SelectOptimize().AsEnumerable().ToList();
                string sfalse = "";
                for (int i = 1; i <= Request.GetNumber("count"); i++)
                {
                   
                    
                    idv.ChangeType = 1; // Nhap kho
                    idv.UserID = new Guid(Session["UserID"].ToString());

                    idv.ProductCode = (Request.Get("hdfproductCode-" + i.ToString()));
                    if (!idv.CheckProductCode())
                    {
                        sfalse += idv.ProductCode.ToString() +",";
                    }
                    else
                    {
                        idv.QuantityChange = (Request.GetNumber("hdfQuantity-" + i.ToString()));
                        idv.BrokenQuantityChange = (Request.GetNumber("hdfBrokenQuantity-" + i.ToString()));

                        idv.InventoryID = Request.IsNull("slSearchGroup") ? idv.InventoryID = new Guid(listIv[0]["InventoryID"].ToString()) : idv.InventoryID = new Guid(Request.Get("slSearchGroup"));

                        idv.Description = "Thay đổi số lượng kho bởi chức năng import dữ liệu";

                        idv.ImportChange();
                    }
                 
                }
                if (sfalse!="")
                    Libs.WriteError("Mã sản phẩm import false: " + sfalse.ToString());
                return Redirect("/Webcms/InventoryDetail/ImportProduct?result=1&sfalse=" + sfalse);
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/InventoryDetail/ImportProduct?error=1");
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
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
    public class WarrantyController : DefaultController
    {
        private String per = Libs.GetPermission("PRODUCT");
        public ActionResult Index()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");

            clsCMS_Warranty w = new clsCMS_Warranty();

            //InventoryViewModel inv = new InventoryViewModel();
            //string strUserId = (string)Session["UserID"];
            //inv.UserID = new SqlGuid(strUserId);
            //List<DataRow> listIv = inv.SelectUserMenu2().AsEnumerable().ToList();
            //ViewBag.Inventory = listIv;
            //if (listIv.Count > 0)
            //{
            //    ViewBag.InventoryId = listIv[0]["value"];
            //    w.InventoryID = ViewBag.InventoryId;
            //}
            //if (!Request.IsNull("slSearchInventory"))
            //    w.InventoryID = new Guid(Request.Get("slSearchInventory"));

            return View();
        }

        public ActionResult WarrantyProductDetail()
        {
            LoadDefault();

            if (per.IndexOf("I") < 0)
                return View("NotPermission");

            //Edit
            if (!Request.IsNull("ID"))
            {
                clsCMS_WarrantyProduct wp = new clsCMS_WarrantyProduct();
                UserViewModel u = new UserViewModel();

                wp.ID = new Guid(Request.Get("ID"));
                ViewBag.Edit = wp.SelectOne().AsEnumerable().ToList();
                u.PageIndex = 1;
                u.PageSize = 1000;
                ViewBag.Users = u.SelectAll().AsEnumerable().ToList();
            }
//            else
//                return RedirectToAction("Index");

            return View();
        }

        public ActionResult UpdateWarrantyProductDetail()
        {
            try{
                if (per.IndexOf("I") < 0)
                    return View("NotPermission");

                //Edit
                if (!Request.IsNull("ID"))
                {
                    clsCMS_WarrantyProduct wp = new clsCMS_WarrantyProduct();
                    UserViewModel u = new UserViewModel();

                    wp.ID = new Guid(Request.Get("ID"));
                    wp.ProductName = Request.Get("txtProductName");
                    wp.ProductID = new Guid(Request.Get("txtProductID"));
                    wp.WarrantyID = new Guid(Request.Get("txtWarrantyID"));
                    var bd = Request.Get("txtPurchaseDate");
                    if (bd.Length > 0)
                    {
                        string[] bdArray = bd.Split('/');
                        wp.PurchaseDate = new SqlDateTime(int.Parse(bdArray[2]), int.Parse(bdArray[1]), int.Parse(bdArray[0]));
                    }
                    wp.WarrantyStatus = Request.Get("txtWarrantyStatus");
                    wp.Description = Request.Get("txtDescription");
                    wp.FixerID = new Guid(Request.Get("slFixer"));
                    wp.Update();
                }
                return Redirect("/Webcms/Warranty/WarrantyProductDetail?ID=" + Request.Get("ID") + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Warranty/WarrantyProductDetail?ID=" + Request.Get("ID") + "&error=1");
            }
        }

        public ActionResult Detail()
        {
            LoadDefault();

            if (per.IndexOf("I") < 0)
                return View("NotPermission");

            UserViewModel u = new UserViewModel();
            u.PageIndex = 1;
            u.PageSize = 1000;
            ViewBag.Users = u.SelectAll().AsEnumerable().ToList();

            //select inventory
            //string strUserId = (string)Session["UserID"];            
            //InventoryViewModel inv = new InventoryViewModel();
            //inv.UserID = new SqlGuid(strUserId);
            //ViewBag.MenuInv = inv.SelectUserMenu().AsEnumerable().ToList();

            //select inventory
            string strUserId = (string)Session["UserID"];     
            InventoryViewModel inv = new InventoryViewModel();
            inv.UserID = new SqlGuid(strUserId);
            ViewBag.MenuInv = inv.SelectUserMenu1().AsEnumerable().ToList();

            ShipperViewModel sv = new ShipperViewModel();
            ViewBag.Shipper = sv.SelectOptimize().AsEnumerable().ToList();

            //Edit
            if (!Request.IsNull("ID"))
            {
                clsCMS_Warranty w = new clsCMS_Warranty();
                clsCMS_WarrantyProduct wp = new clsCMS_WarrantyProduct();

                w.ID = new Guid(Request.Get("ID"));
                wp.WarrantyID = new Guid(Request.Get("ID"));
                ViewBag.Edit = w.SelectOne().AsEnumerable().ToList();

            }
//            else
//                return RedirectToAction("Index");

            return View();
        }
        public string DeleteTicket(string ticketId)
        {
            try
            {
                LoadDefault();

                if (per.IndexOf("D") < 0)
                    return string.Empty;

                clsCMS_Warranty w = new clsCMS_Warranty();
                w.ID = new Guid(ticketId);
                w.Delete();
                return "1";
            }
            catch (Exception e)
            {
                return "0";
            }
        }
        public string InsertWarrantyProduct(string ticketId, string productId, string productName, string buyDate, string warrantyStatus, string description, string quantity, string price, string fixer, string wproductid)
        {
            try
            {
                LoadDefault();

                if (per.IndexOf("D") < 0)
                    return string.Empty;

                clsCMS_WarrantyProduct wp = new clsCMS_WarrantyProduct();
                wp.WarrantyID = new Guid(ticketId);
                wp.WarrantyUserID = new Guid(Session["UserID"].ToString());
                if (!string.IsNullOrEmpty(productId))
                    wp.ProductID = new Guid(productId);
                else
                    wp.ProductID = SqlGuid.Null;
                if (!string.IsNullOrEmpty(productName))
                    wp.ProductName = productName;
                else
                    wp.ProductName = "";
                if (!string.IsNullOrEmpty(buyDate))
                {
                    string[] bdArray = buyDate.Split('/');
                    wp.PurchaseDate = new SqlDateTime(int.Parse(bdArray[2]), int.Parse(bdArray[1]),
                        int.Parse(bdArray[0]));
                }
                else
                    wp.PurchaseDate = SqlDateTime.Null;
                if (!string.IsNullOrEmpty(warrantyStatus))
                    wp.WarrantyStatus = warrantyStatus;
                else
                    wp.WarrantyStatus = "";
                if (!string.IsNullOrEmpty(description))
                    wp.Description = description;
                else
                    wp.Description = "";
                if (!string.IsNullOrEmpty(quantity))
                    wp.Quantity = Int32.Parse(quantity);
                else
                    wp.Quantity = 0;
                if (!string.IsNullOrEmpty(price))
                    wp.Price = Decimal.Parse(price);
                else
                    wp.Price = 0;
                if (!string.IsNullOrEmpty(fixer))
                    wp.FixerID = new Guid(fixer);
                else 
                    wp.FixerID = SqlGuid.Null;
                if (string.IsNullOrEmpty(wproductid))
                {
                    wp.ID = Guid.NewGuid();
                    wp.Insert();
                }
                else
                {
                    wp.ID = new Guid(wproductid);
                    wp.Update();
                }
                return "1";
            }
            catch (Exception e)
            {
                return "0";
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create()
        {
            try
            {
                CheckUser();
                clsCMS_Warranty w = new clsCMS_Warranty();
                w.UserID = new Guid(Session["UserID"].ToString());
                w.Phone = Request.Get("txtPhone");
                w.Name = Request.Get("txtFullName");
                w.Status = Request.GetNumber("slStatus");
                w.Address = Request.Get("txtAddress");
                w.Email = Request.Get("txtEmail");
                if (!Request.IsNull("slOrderInv"))
                    w.InventoryID = new Guid(Request.Get("slOrderInv"));
                if (!Request.IsNull("slsShipper"))
                    w.ShipperID = new Guid(Request.Get("slsShipper"));
                if (Request.IsNull("ID"))
                {
                    if (per.IndexOf("I") < 0)
                        return View("NotPermission");
                    w.ID = Guid.NewGuid();
                    w.DateCreated = DateTime.Now;
                    w.Insert();
                }
                else
                {
                    if (per.IndexOf("U") < 0)
                        return View("NotPermission");
                    w.ID = new Guid(Request.Get("ID"));
                    if (w.Status == 6)
                        w.DateComplete = DateTime.Now;
                    w.Update();
                    
                }

                return Redirect("/Webcms/Warranty/Detail?ID=" + w.ID + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Warranty/Detail?ID=" + Request.Get("ID") + "&error=1");
            }
        }

        public string GetListWarrantyTickets(int? ticketStatus, Guid? inv)
        {
            if (per.IndexOf("S") < 0 || ticketStatus == null)
                return "Not Permission";
            
            clsCMS_Warranty warrantyModel = new clsCMS_Warranty();
            //warrantyModel.InventoryID = inv; 
            DataTable list = warrantyModel.SelectAll();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in list.Rows)
            {
                if ((int) dr["Status"] == ticketStatus)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in list.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
            }
            return serializer.Serialize(rows);
        }

        public string GetProductByCode(string pCode)
        {
            ProductViewModel pv = new ProductViewModel();
            pv.Title = pCode;
            DataTable dt = pv.GetSuggestWarrantyProduct();
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

        public string GetWarrantyProductByTicketsId(string ticketId)
        {
            if (per.IndexOf("S") < 0 )
                return "Not Permission";

            clsCMS_WarrantyProduct warrantyModel = new clsCMS_WarrantyProduct();
            if(!string.IsNullOrEmpty(ticketId))
                warrantyModel.WarrantyID = new Guid(ticketId);
            DataTable list = warrantyModel.SelectByWarrantyId();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in list.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in list.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        public string GetWarrantyListByPhone(string phone)
        {
            if (per.IndexOf("S") < 0)
                return "Not Permission";

            clsCMS_Warranty w = new clsCMS_Warranty();
            if (!string.IsNullOrEmpty(phone))
                w.Phone = phone;
            DataTable list = w.SelectByPhone();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in list.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in list.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
    }
}

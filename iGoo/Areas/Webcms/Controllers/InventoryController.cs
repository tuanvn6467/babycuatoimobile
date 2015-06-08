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

namespace iGoo.Areas.Webcms.Controllers
{
    public class InventoryController : DefaultController
    {
        //
        // GET: /Webcms/Inventory/
        private String per = Libs.GetPermission("INVENTORY");
        public ActionResult Index()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            InventoryViewModel iv = new InventoryViewModel();


            if (!Request.IsNull("txtKey"))
                iv.InventoryName = Request.Get("txtKey");
            if (!Request.IsNull("slSearchStatus"))
                iv.Status = Request.GetNumber("slSearchStatus");
            iv.InventoryID = Request.IsNull("InventoryID") ? iv.InventoryID = Guid.Empty : iv.InventoryID = new Guid(Request.Get("InventoryID"));

            iv.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            iv.PageSize = Request.IsNull("show") ? 20 : Request.GetNumber("show");

            if (!Request.IsNull("OrderBy") && !Request.IsNull("Order"))
                iv.OrderBy = Request.Get("OrderBy") + " " + Request.Get("Order");


            //edit
            if (!Request.IsNull("InventoryID"))
            {
                iv.InventoryID = new Guid(Request.Get("InventoryID"));
                ViewBag.Edit = iv.SelectOne().AsEnumerable().ToList();
            }


            //Page
            List<DataRow> list = iv.SelectAll().AsEnumerable().ToList();
            ViewBag.Inventory = list;
            ViewBag.TotalPages = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"]) / (double)iv.PageSize) : 0;
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create()
        {
            try 
            { 
                InventoryViewModel iv = new InventoryViewModel();
                
                iv.InventoryName = Request.Get("txtInventoryName");
                iv.InventoryCode = Request.Get("txtInventoryCode");
                iv.InventoryDeputy = Request.Get("txtInventoryDeputy");
                iv.InventoryDesc = Request.Get("txtInventoryDesc");
                iv.InventoryLocation = Request.Get("txtInventoryLocation");
                iv.CityArr = Request.Get("txtCityArr");
                iv.Status = Request.GetNumber("slStatus");
               
                if (Request.IsNull("InventoryID"))
                {
                    if (per.IndexOf("I") < 0)
                        return View("NotPermission");
                    iv.InventoryID = Guid.NewGuid();
                    iv.Insert();
                }
                else
                {
                    if (per.IndexOf("U") < 0)
                        return View("NotPermission");
                    iv.InventoryID = new Guid(Request.Get("InventoryID"));
                    iv.Update();
                }

                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/Inventory" + returnUrl + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Inventory?error=1");
            }
        }

        [HttpPost]
        public ActionResult Update()
        {
            try
            {
                if (per.IndexOf("U") < 0)
                    return View("NotPermission");
                InventoryViewModel iv = new InventoryViewModel();
                for (int i = 1; i <= Request.GetNumber("count"); i++)
                {
                    if (!Request.IsNull("ckID-" + i.ToString()))
                    {
                        iv.Status = (Request.GetNumber("slStatus-" + i.ToString()));
                        iv.Update();
                    }
                }

                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/Inventory" + returnUrl + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Inventory?error=1");
            }
        }

        [HttpPost]
        public ActionResult Delete()
        {
            try
            {
                if (per.IndexOf("D") < 0)
                    return View("NotPermission");
                InventoryViewModel iv = new InventoryViewModel();
                for (int i = 1; i <= Request.GetNumber("count"); i++)
                {
                    if (!Request.IsNull("ckID-" + i.ToString()))
                    {
                        iv.InventoryID = new Guid(Request.Get("ckID-" + i.ToString()));
                        iv.Delete();
                    }
                }

                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/Inventory" + returnUrl + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Inventory?error=1");
            }
        }

        private String ReplaceSpace(String str)
        {
            if (str.IndexOf("  ") > -1)
            {
                str = str.Replace("  ", " ");
                return ReplaceSpace(str);
            }
            return str;
        }
    }
}

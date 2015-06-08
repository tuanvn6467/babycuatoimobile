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
    public class NVGHController : DefaultController
    {
        //
        // GET: /Webcms/NVGH/
        private String per = Libs.GetPermission("SHIPPER");
        public ActionResult Index()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            NVGHViewModel iv = new NVGHViewModel();


            if (!Request.IsNull("txtKey"))
                iv.FullName = Request.Get("txtKey");
            if (!Request.IsNull("slSearchStatus"))
                iv.Status = Request.GetNumber("slSearchStatus");
            //iv.ShipperID = Request.IsNull("ShipperID") ? iv.ShipperID = Guid.Empty : iv.ShipperID = new Guid(Request.Get("ShipperID"));

            iv.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            iv.PageSize = Request.IsNull("show") ? 20 : Request.GetNumber("show");

            if (!Request.IsNull("OrderBy") && !Request.IsNull("Order"))
                iv.OrderBy = Request.Get("OrderBy") + " " + Request.Get("Order");

            //edit
            if (!Request.IsNull("ShipperID"))
            {
                iv.ShipperID = new Guid(Request.Get("ShipperID"));
                ViewBag.Edit = iv.SelectOne().AsEnumerable().ToList();
            }

            //Page
            List<DataRow> list = iv.SelectAll().AsEnumerable().ToList();
            ViewBag.NVGH = list;
            ViewBag.TotalPages = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"]) / (double)iv.PageSize) : 0;
            return View();           
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create()
        {
            try
            {
                NVGHViewModel iv = new NVGHViewModel();

                iv.FullName = Request.Get("txtFullName");
                iv.ShipperCode = Request.Get("txtShipperCode");
                iv.Phone = Request.Get("txtPhone");
                iv.Gender = Request.GetNumber("slGender");
                iv.Address = Request.Get("txtAddress");
                iv.Email = Request.Get("txtEmail");
                iv.Info = Request.Get("txtInfo");
                iv.Status = Request.GetNumber("slStatus");
                iv.Created = DateTime.Now; 

                if (Request.IsNull("ShipperID"))
                {
                    if (per.IndexOf("I") < 0)
                        return View("NotPermission");
                    iv.ShipperID = Guid.NewGuid();
                    iv.Insert();
                }
                else
                {
                    if (per.IndexOf("U") < 0)
                        return View("NotPermission");
                    iv.ShipperID = new Guid(Request.Get("ShipperID"));
                    iv.Update();
                }

                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/NVGH" + returnUrl + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/NVGH?error=1");
            }
        }

        [HttpPost]
        public ActionResult Update()
        {
            try
            {
                if (per.IndexOf("U") < 0)
                    return View("NotPermission");
                NVGHViewModel iv = new NVGHViewModel();
                for (int i = 1; i <= Request.GetNumber("count"); i++)
                {
                    if (!Request.IsNull("ckID-" + i.ToString()))
                    {
                        iv.ShipperID = new Guid(Request.Get("ckID-" + i.ToString()));
                        iv.Status = (Request.GetNumber("slStatus-" + i.ToString()));
                        iv.Update();
                    }
                }                

                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/NVGH" + returnUrl + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/NVGH?error=1");
            }
        }

        [HttpPost]
        public ActionResult Delete()
        {
            try
            {
                if (per.IndexOf("D") < 0)
                    return View("NotPermission");
                NVGHViewModel iv = new NVGHViewModel();
                for (int i = 1; i <= Request.GetNumber("count"); i++)
                {
                    if (!Request.IsNull("ckID-" + i.ToString()))
                    {
                        iv.ShipperID = new Guid(Request.Get("ckID-" + i.ToString()));
                        iv.Delete();
                    }
                }

                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/NVGH" + returnUrl + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/NVGH?error=1");
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

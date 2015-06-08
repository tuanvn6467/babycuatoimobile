using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using iGoo.Areas.Webcms.Models;
using iGoo.Helpers;
using System.Data;
using iGoo.Classes;

namespace iGoo.Areas.Webcms.Controllers
{
    public class VasController : DefaultController
    {
        private String per = Libs.GetPermission("ORDER");
        public ActionResult Index()
        {
            LoadDefault();

            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            VasViewModel vv = new VasViewModel();
            if (!Request.IsNull("txtKey"))
                vv.Name = Request.Get("txtKey");
            if (!Request.IsNull("slSearchStatus"))
                vv.Status = Request.GetNumber("slSearchStatus");
            vv.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            vv.PageSize = Request.IsNull("show") ? 20 : Request.GetNumber("show");
            List<DataRow> list = vv.SelectAll().AsEnumerable().ToList();
            ViewBag.VasList = list;
            ViewBag.TotalPages = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"]) / (double)vv.PageSize) : 0;
            return View();
        }

        public ActionResult ProcessVas()
        {
            LoadDefault();

            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            string btnText = "Thêm mới";
            string btnId = "btnAddVas";
            if (!Request.IsNull("Id"))
            {
                btnText = "Cập nhật";
                btnId = "btnUpdateVas";
                VasViewModel vv = new VasViewModel();
                vv.ID = new Guid(Request.Get("Id"));
                ViewBag.Edit = vv.SelectOne().AsEnumerable().ToList();
            }
            ViewBag.btnText = btnText;
            ViewBag.btnId = btnId;
            return View();
       }

        [HttpPost]
        public ActionResult Update()
        {
            try
            {
                if (per.IndexOf("U") < 0)
                    return View("NotPermission");
                VasViewModel vv = new VasViewModel();
                vv.ID = new Guid(Request.Get("vasId"));
                vv.Code = Request.Get("txtCode");
                vv.Name = Request.Get("txtName");
                vv.Price = Int32.Parse(Request.Get("txtPrice").Replace(",",string.Empty));
                vv.Status = Request.GetNumber("slStatus");
                vv.Description = Request.Get("txtDescription");
                vv.Update();
                

                return Redirect("/Webcms/VAS/ProcessVas?Id=" + Request.Get("VasId") + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/VAS/ProcessVas?Id=" + Request.Get("VasId") + "&error=1");
            }
        }

        [HttpPost]
        public ActionResult Delete()
        {
            try
            {
                if (per.IndexOf("D") < 0)
                    return View("NotPermission");
                VasViewModel vv = new VasViewModel();
                int count = Request.GetNumber("count");
                for (int i = 1; i <= count; i++)
                {
                    if (!Request.IsNull("ckID-" + i.ToString()))
                    {
                        vv.ID = new Guid(Request.Get("ckID-" + i.ToString()));
                        vv.Delete();
                    }
                }

                return Redirect("/Webcms/VAS?result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/VAS?error=1");
            }
        }
        [HttpPost]
        public ActionResult AddNew()
        {
            try
            {
                VasViewModel vv = new VasViewModel();
                vv.ID = Guid.NewGuid();
                vv.Code = Request.Get("txtCode");
                vv.Name = Request.Get("txtName");
                vv.Price = Request.GetNumber("txtPrice");
                vv.Status = Request.GetNumber("slStatus");
                vv.Description = Request.Get("txtDescription");
                vv.Insert();

                return Redirect("/Webcms/VAS/ProcessVas?Id="+vv.ID+"&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/VAS/ProcessVas?&error=1");
            }
        }

        public string GetProductByCode(string pCode)
        {
            VasViewModel vv = new VasViewModel();
            vv.Name = pCode;
            DataTable dt = vv.GetSuggestVas();
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
            return serializer.Serialize(rows); ;
        }
    }
}

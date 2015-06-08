using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using iGoo.Helpers;
using iGoo.Classes;
using iGoo.Areas.Webcms.Models;

namespace iGoo.Areas.Webcms.Controllers
{
    public class TemplateController : DefaultController
    {
        public ActionResult Index()
        {
            CheckUser();
            //Select default
            LoadDefault();
            //Select user
            ViewBag.LoginUserID = Session["UserID"];
            ViewBag.LoginFullName = Session["FullName"];

            StylesheetViewModel st = new StylesheetViewModel();
            st.Name = "";
            List<DataRow> list = st.SelectAll().AsEnumerable().ToList();
            ViewBag.Stylesheet1 = list;

            TemplateViewModel tpl = new TemplateViewModel();
            tpl.Name = "";
            List<DataRow> list1 = tpl.SelectAll().AsEnumerable().ToList();
            ViewBag.Template1 = list1;

            return View();
        }
        


        [HttpPost]
        public ActionResult CreateTemplate()
        {
            try
            {
                clsCMS_Templates templ = new clsCMS_Templates();
                templ.TemplateId = Guid.NewGuid();
                templ.Name = Request.Get("templateName");
                templ.Des = Request.Get("templateDes");
                string src = Server.MapPath("~/Views/Layout/" + templ.Name.ToString()+".cshtml");
                if (System.IO.File.Exists(src))
                {
                    templ.Insert();
                    return Redirect("/Webcms/Template?result=1");
                }
                else
                {
                    return Redirect("/Webcms/Template?error=2");
                }
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Template?error=1");
            }
        }

        [HttpPost]
        public string DeleteTemplate(string Id)
        {
            clsCMS_Templates tpl = new clsCMS_Templates();
            tpl.TemplateId = new SqlGuid(Id);
            string rtn = "";
            if (tpl.Delete())
            {
                rtn = "thanh cong";
            }
            else
            {
                rtn = "that bai";
            }
            return rtn;
        }

        [HttpPost]
        public ActionResult CreateStylesheet()
        {
            try
            {
                clsCMS_Stylesheets stylesheet = new clsCMS_Stylesheets();
                stylesheet.StylesheetID = Guid.NewGuid();
                stylesheet.Name = Request.Get("styleName");
                stylesheet.Des = Request.Get("styleDes");
                string src = Server.MapPath("~/Source/images/" + stylesheet.Name.ToString() + ".css");
                if (System.IO.File.Exists(src))
                {
                    stylesheet.Insert();
                    return Redirect("/Webcms/Template?result1=1");
                }
                else
                {
                    return Redirect("/Webcms/Template?error1=2");
                }
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Template?error1=1");
            }
        }

        public string DeleteStylesheet(string Id)
        {
            clsCMS_Stylesheets stl = new clsCMS_Stylesheets();
            stl.StylesheetID = new SqlGuid(Id);
            string rtn = "";
            if (stl.Delete())
            {
                rtn = "thanh cong";
            }
            else
            {
                rtn = "that bai";
            }
            return rtn;
        }

        public string ActiveTemplate(string Id)
        {
            clsCMS_Templates tpl = new clsCMS_Templates();
            tpl.TemplateId = new SqlGuid(Id);
            string rtn = "";
            if (tpl.Update_Status())
            {
                rtn = "thanh cong";
            }
            else
            {
                rtn = "that bai";
            }
            return rtn;
        }

        public string ActiveStylesheet(string Id)
        {
            clsCMS_Stylesheets stl = new clsCMS_Stylesheets();
            stl.StylesheetID = new SqlGuid(Id);
            string rtn = "";
            if (stl.Update_Status())
            {
                rtn = "thanh cong";
            }
            else
            {
                rtn = "that bai";
            }
            return rtn;
        }
    }
}

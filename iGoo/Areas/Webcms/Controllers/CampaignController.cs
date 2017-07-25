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
    public class CampaignController : DefaultController
    {
        private String per = Libs.GetPermission("ORDER");
        public ActionResult Index()
        {
            LoadDefault();

            if (per.IndexOf("S") < 0)
                return View("NotPermission");

            CampaignViewModel vv = new CampaignViewModel();
            if (!Request.IsNull("txtKey"))
                vv.Name = Request.Get("txtKey");
            if (!Request.IsNull("slSearchStatus"))
                vv.Status = Request.GetNumber("slSearchStatus");
            
            vv.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            vv.PageSize = Request.IsNull("show") ? 20 : Request.GetNumber("show");
            List<DataRow> list = vv.SelectAll().AsEnumerable().ToList();
            ViewBag.CampaignList = list;
            ViewBag.TotalPages = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"]) / (double)vv.PageSize) : 0;
            return View();
        }

        public ActionResult ProcessCampaign()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            string btnText = "Thêm mới";
            string btnId = "btnAddCampaign";

            //Slect group
            AttributeViewModel at = new AttributeViewModel();
            at.Code = "ATTRIBUTE_MEMBER_GROUP";
            ViewBag.GroupName = at.SelectChild().AsEnumerable().ToList();

            if (!Request.IsNull("Id"))
            {
                btnText = "Cập nhật";
                btnId = "btnUpdateCampaign";
                CampaignViewModel vv = new CampaignViewModel();
                vv.ID = new Guid(Request.Get("Id"));
                if (!Request.IsNull("slGroupID"))
                    vv.GroupID = new Guid(Request.Get("slGroupID"));
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
                CampaignViewModel vv = new CampaignViewModel();
                vv.ID = new Guid(Request.Get("Id"));                
                vv.Name = Request.Get("txtName");
                vv.Subject = Request.Get("txtSubject");
                vv.Body = Request.Get("txtBody"); 
                vv.Status = Request.GetNumber("slStatus");                
                vv.Update();


                return Redirect("/Webcms/Campaign/ProcessCampaign?Id=" + Request.Get("Id") + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Campaign/ProcessCampaign?Id=" + Request.Get("Id") + "&error=1");
            }
        }

        [HttpPost]
        public ActionResult Delete()
        {
            try
            {
                if (per.IndexOf("D") < 0)
                    return View("NotPermission");
                CampaignViewModel vv = new CampaignViewModel();
                int count = Request.GetNumber("count");
                for (int i = 1; i <= count; i++)
                {
                    if (!Request.IsNull("ckID-" + i.ToString()))
                    {
                        vv.ID = new Guid(Request.Get("ckID-" + i.ToString()));
                        vv.Delete();
                    }
                }

                return Redirect("/Webcms/Campaign?result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Campaign?error=1");
            }
        }

        [HttpPost]
        public ActionResult AddNew()
        {
            try
            {
                CampaignViewModel vv = new CampaignViewModel();
                vv.ID = Guid.NewGuid();                
                vv.Name = Request.Get("txtName");
                vv.Subject = Request.Get("txtSubject");
                vv.Body = Request.Get("txtBody");
                vv.GroupID = new Guid(Request.Get("slGroupID"));
                vv.Status = Request.GetNumber("slStatus");
                vv.DateCreated = DateTime.Now;
                vv.Insert();

                return Redirect("/Webcms/Campaign/ProcessCampaign?Id=" + vv.ID + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Campaign/ProcessCampaign?&error=1");
            }
        }

        [HttpPost]
        public ActionResult TestEmail()
        {
            try
            {
                if (per.IndexOf("U") < 0)
                    return View("NotPermission");
                CampaignViewModel vv = new CampaignViewModel();
                vv.ID = new Guid(Request.Get("Id"));    
                string MailTo = Request.Get("txtTestEmail");
                string Subject = Request.Get("txtSubject");
                string Body = Request.Get("txtBody");               
                
                vv.TestEmail(Subject, Body, MailTo);                

                //return Redirect("/Webcms/Campaign/ProcessCampaign");
                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/Campaign/ProcessCampaign?ID=" + vv.ID + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Campaign/ProcessCampaign");
            }
        }
    }
}

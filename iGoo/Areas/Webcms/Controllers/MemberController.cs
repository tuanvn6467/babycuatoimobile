using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iGoo.Areas.Webcms.Models;
using iGoo.Helpers;
using System.Data;
using iGoo.Classes;

namespace iGoo.Areas.Webcms.Controllers
{
    public class MemberController : DefaultController
    {
        private String per = Libs.GetPermission("MEMBER");
        public ActionResult Index()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            /*
            CategoryViewModel cvm = new CategoryViewModel();
            cvm.Code = "CATEGORY_ANSWER";
            ViewBag.PerAnswer = cvm.SelectChild().AsEnumerable().ToList();
            cvm.Code = "CATEGORY_GOOGLE";
            ViewBag.PerNew = cvm.SelectChild().AsEnumerable().ToList();
            */

            //Slect group
            AttributeViewModel at = new AttributeViewModel();
            at.Code = "ATTRIBUTE_MEMBER_GROUP";
            ViewBag.GroupName = at.SelectChild().AsEnumerable().ToList();

            //Select user
            MemberViewModel mvm = new MemberViewModel();
            if (!Request.IsNull("txtKey"))
                mvm.UserName = Request.Get("txtKey");
            if (!Request.IsNull("slGroupID"))
                mvm.GroupID = new Guid(Request.Get("slGroupID"));
            if (!Request.IsNull("slSearchStatus"))
                mvm.Status = Request.GetNumber("slSearchStatus");

            mvm.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            mvm.PageSize = Request.IsNull("show") ? 20 : Request.GetNumber("show");


            if (!Request.IsNull("OrderBy") && !Request.IsNull("Order"))
                mvm.OrderBy = Request.Get("OrderBy") + " " + Request.Get("Order");

/*
            //Eidt
            if (!Request.IsNull("ID"))
            {
                mvm.UserID = new Guid(Request.Get("ID"));
                ViewBag.Edit = mvm.SelectOne().AsEnumerable().ToList();
            }*/

            //Page
            List<DataRow> list = mvm.SelectAll().AsEnumerable().ToList();
            ViewBag.Member = list;
            ViewBag.TotalPages = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"]) / (double)mvm.PageSize) : 0;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create()
        {
            try
            {
                if (per.IndexOf("U") < 0)
                    return View("NotPermission");
                MemberViewModel u = new MemberViewModel();
                if (Request.Get("ID").Length > 0)
                {
                    u.UserID = new Guid(Request.Get("ID"));
                }
                else
                {
                    u.UserID = Guid.NewGuid();
                }
                u.Status = Request.GetNumber("slStatus");
                if (u.Status != 2)
                {
                    u.Phone = Request.Get("txtPhone");
                    u.Phone1 = Request.Get("txtPhone1");
                    u.Phone2 = Request.Get("txtPhone2");
                    u.Email = Request.Get("txtEmail");
                    u.SendEmail = Request.Get("sendEmail").Length > 0 ? 1 : 0;
                    u.TaxNumber = Request.Get("txtTaxNumber");
                    u.UserName = Request.Get("txtUserName");
                    u.FullName = Request.Get("txtFullName");
                    u.Gender = Request.Get("sex").Length == 0 || Request.Get("sex").Equals("female") ? 0 : 1;
                    var bd = Request.Get("txtBrithday");
                    if (bd.Length > 0)
                    {
                        string[] bdArray = bd.Split('/');
                        u.Brithday = new SqlDateTime(int.Parse(bdArray[2]), int.Parse(bdArray[1]), int.Parse(bdArray[0]));
                    }
                    u.Address = Request.Get("txtAddress");
                    u.GroupID = new Guid(Request.Get("slGroupID"));
                    u.Status = int.Parse(Request.Get("slStatus"));
                    u.Permission = Request.Get("ckPermission").Replace(',', '#');
                    if (!Request.Get("txtPassword").Equals("password") && !Request.IsNull("txtPassword"))
                        u.Password = Libs.sMD5(Request.Get("txtPassword"));
                    if (Request.Get("ID").Length > 0)
                    {
                        u.Update();
                        SaveUserLog(UserForm.Member.ToString(), UserActionType.Update.ToString(), "Cập nhật Khách hàng: " + u.FullName.ToString());
                    }
                    else
                    {
                        u.Insert();
                        SaveUserLog(UserForm.Member.ToString(), UserActionType.Insert.ToString(), "Thêm mới Khách hàng: " + u.FullName.ToString());
                    }
                }
                else
                    u.UpdateMemberSpam();

                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/Member/Detail?ID=" + u.UserID + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Member/Detail?ID=" + Request.Get("ID") +"&error=1");
            }
        }

        [HttpPost]
        public ActionResult Update()
        {
            try
            {
                if (per.IndexOf("U") < 0)
                    return View("NotPermission");
                MemberViewModel u = new MemberViewModel();
                for (int i = 1; i <= Request.GetNumber("count"); i++)
                {
                    if (!Request.IsNull("ckID-" + i.ToString()))
                    {
                        u.Status = Request.GetNumber("slStatus-" + i.ToString());
                        if (u.Status == 2)
                            continue;
                        u.GroupID = new Guid(Request.Get("slGroup-" + i.ToString()));
                        u.UserID = new Guid(Request.Get("ckID-" + i.ToString()));
                        u.Update();
                        SaveUserLog(UserForm.Member.ToString(), UserActionType.Update.ToString(), "Cập nhật khách hàng: " + u.SelectOne().Rows[0]["Name"].ToString());
                    }
                }
                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/Member" + returnUrl + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Member?error=1");
            }
        }

        [HttpPost]
        public ActionResult Delete()
        {
            try
            {
                if (per.IndexOf("D") < 0)
                    return View("NotPermission");
                MemberViewModel u = new MemberViewModel();
                for (int i = 1; i <= Request.GetNumber("count"); i++)
                {
                    if (!Request.IsNull("ckID-" + i.ToString()))
                    {
                        u.UserID = new Guid(Request.Get("ckID-" + i.ToString()));
                        var d = u.SelectOne();
                        u.Delete();
                        SaveUserLog(UserForm.Member.ToString(), UserActionType.Delete.ToString(), "Xóa khách hàng: " + d.Rows[0]["FullName"].ToString());
                    }
                }
                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/Member" + returnUrl + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Member?error=1");
            }
        }
        public ActionResult Detail()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            var mvm = new MemberViewModel();
            //Select group
            AttributeViewModel at = new AttributeViewModel();
            at.Code = "ATTRIBUTE_MEMBER_GROUP";
            ViewBag.GroupName = at.SelectChild().AsEnumerable().ToList();
            if (!Request.IsNull("ID"))
            {
                mvm.UserID = new Guid(Request.Get("ID"));
                ViewBag.CustomerId = Request.Get("ID");
                ViewBag.Edit = mvm.SelectOne().AsEnumerable().ToList();
            }
            /*else
            {
                return Redirect("/Webcms/Member");
            }*/
            return View();
        }
        public string GetOrdersByCustomerId(string customerId)
        {
            LoadDefault();

            if (per.IndexOf("S") < 0)
                return string.Empty;
            
            OrderViewModel ov = new OrderViewModel();
            ov.UserID = new SqlGuid(customerId);

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;
            DataTable dt = ov.SelectOrdersByCustomerId();
            if (dt == null)
                return string.Empty;
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
        public string GetProductsByCustomerId(string customerId)
        {
            LoadDefault();

            if (per.IndexOf("S") < 0)
                return string.Empty;
            
            OrderViewModel ov = new OrderViewModel();
            ov.UserID = new SqlGuid(customerId);

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;
            DataTable dt = ov.SelectProductsByCustomerId();
            if (dt == null)
                return string.Empty;
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
        public ActionResult MembersList()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            /*
            CategoryViewModel cvm = new CategoryViewModel();
            cvm.Code = "CATEGORY_ANSWER";
            ViewBag.PerAnswer = cvm.SelectChild().AsEnumerable().ToList();
            cvm.Code = "CATEGORY_GOOGLE";
            ViewBag.PerNew = cvm.SelectChild().AsEnumerable().ToList();
            */

            //Select group
            AttributeViewModel at = new AttributeViewModel();
            at.Code = "ATTRIBUTE_MEMBER_GROUP";
            ViewBag.GroupName = at.SelectChild().AsEnumerable().ToList();

            //Select user
            MemberViewModel mvm = new MemberViewModel();
            if (!Request.IsNull("txtKey"))
                mvm.UserName = Request.Get("txtKey");
            if (!Request.IsNull("slGroupID"))
                mvm.GroupID = new Guid(Request.Get("slGroupID"));
            if (!Request.IsNull("slSearchStatus"))
                mvm.Status = Request.GetNumber("slSearchStatus");

            mvm.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            mvm.PageSize = Request.IsNull("show") ? 20 : Request.GetNumber("show");


            if (!Request.IsNull("OrderBy") && !Request.IsNull("Order"))
                mvm.OrderBy = Request.Get("OrderBy") + " " + Request.Get("Order");

/*
            //Edit
            if (!Request.IsNull("ID"))
            {
                mvm.UserID = new Guid(Request.Get("ID"));
                ViewBag.Edit = mvm.SelectOne().AsEnumerable().ToList();
            }*/

            //Page
            List<DataRow> list = mvm.SelectAllWithOrder().AsEnumerable().ToList();
            ViewBag.Member = list;
            ViewBag.TotalPages = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"]) / (double)mvm.PageSize) : 0;
            return View();
        }

        public string CreateNewChild(string parentID, string name, string birthDate, int sex)
        {
            try
            {
                LoadDefault();

                if (per.IndexOf("S") < 0)
                    return string.Empty;

                clsCMS_UsersChildren uc = new clsCMS_UsersChildren();
                uc.ParentID = new Guid(parentID);
                uc.ChildID = Guid.NewGuid();
                uc.ChildName = name;
                var bDate = birthDate.Split('/');
                uc.BirthDate = new DateTime(int.Parse(bDate[2]), int.Parse(bDate[1]), int.Parse(bDate[0]));
                uc.Sex = sex != 0;
                uc.Insert();
                return "1";
            }
            catch (Exception e)
            {
                return "0";
            }
        }

        public string DeleteChild(string childId)
        {
            try
            {
                LoadDefault();

                if (per.IndexOf("S") < 0)
                    return string.Empty;

                clsCMS_UsersChildren uc = new clsCMS_UsersChildren();
                uc.ChildID = new Guid(childId);
                uc.Delete();
                return "1";
            }
            catch (Exception e)
            {
                return "0";
            }
        }

        public string GetChildrenByParentId(string parentId)
        {
            try
            {
                LoadDefault();

                if (per.IndexOf("S") < 0)
                    return string.Empty;

                clsCMS_UsersChildren uc = new clsCMS_UsersChildren();
                uc.ParentID = new Guid(parentId);

                System.Web.Script.Serialization.JavaScriptSerializer serializer =
                    new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row = null;
                DataTable dt = uc.SelectOne();
                if (dt == null)
                    return string.Empty;
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
            catch(Exception e)
            {
                return "";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iGoo.Helpers;
using iGoo.Areas.Webcms.Models;
using System.Diagnostics;
using System.Data;

using System.Web.Security;

namespace iGoo.Areas.Webcms.Controllers
{
    public class LoginController : DefaultController
    {
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
                return Redirect("/Webcms/Websites/Blank");
            else
                return View();
        }

        [HttpPost]
        public ActionResult Check()
        {
            UserViewModel uv = new UserViewModel();
            uv.UserName = Request.Get("txtUser");
            uv.Password = Libs.sMD5(Request.Get("txtPassword"));

            DataTable dt = uv.CheckLogin();
            if (dt.Rows.Count>0)
            {
                Session["UserID"] = dt.Rows[0]["UserID"].ToString();
                Session["UserName"] = dt.Rows[0]["UserName"].ToString();
                Session["FullNameAdmin"] = dt.Rows[0]["FullName"].ToString();
                Session["Permission"] = dt.Rows[0]["Permission"].ToString();

                //Membership.CreateUser(Session["UserName"].ToString(), "babycuatoi123123!", "sonln@gmail.com");                
                
                //List<MembershipUser> onlineUsers = new List<MembershipUser>();
                ////var user = UserManager.FindById(User.Identity.GetUserId());
                //foreach (MembershipUser user in Membership.GetAllUsers())                
                //{
                //    if (user.IsOnline)
                //    {
                //        onlineUsers.Add(user);
                //        HttpRuntime.Cache["onlineUsers1"] = onlineUsers;
                //    }
                //}
                return Redirect("/Webcms/Websites/Blank");
            }
            else
                return Redirect("/Webcms?error=1");
        }

        public ActionResult Logout()
        {
            Session["UserID"] = null;
            Session["FullNameAdmin"] = null;
            Session["UserName"] = null;
            return Redirect("/Webcms");
        }
    }
}

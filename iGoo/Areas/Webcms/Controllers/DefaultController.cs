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
    public class DefaultController : Controller
    {
        public void LoadDefault()
        {
            CheckUser();
            ///Select default

            //Select menu
            ModuleViewModel mv = new ModuleViewModel();
            mv.MenuAll = mv.SelectAll();
            ViewBag.Menu = mv.SelectMenu(Guid.Empty);
            //End select default
        }

        public void CheckUser()
        {
            if (Session["UserID"] == null)
                Response.Redirect("/Webcms");
            return;
        }

        public void SaveUserLog(string controller, string action, string comment)
        {
            LogViewModel userLog = new LogViewModel();
            userLog.LogID = Guid.NewGuid();
            userLog.UserID = new Guid(Session["UserID"].ToString());
            userLog.UserName = Session["UserName"].ToString();
            userLog.Created = DateTime.Now;
            userLog.ActionType = action;
            userLog.Form = controller;
            userLog.Comment = comment;
            userLog.Insert();
        }
        public enum UserActionType
        {
            Insert,
            Update,
            Delete,
            Import,
            Export
        };
        //public enum UserForm
        //{
        //    Product,
        //    Category,
        //    Inventory,
        //    Member,
        //    News, 
        //    User
        //};
        public class UserForm
        {
            public const string Product = "QL sản phẩm";
            public const string Category = "QL danh mục";
            public const string Inventory = "QL kho";
            public const string Member = "QL khách hàng";
            public const string News = "QL tin tức";
            public const string User = "QL người dùng";           
        }
    }
}

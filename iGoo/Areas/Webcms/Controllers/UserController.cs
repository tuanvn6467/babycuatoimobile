﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iGoo.Helpers;
using System.Data;
using iGoo.Classes;
using iGoo.Areas.Webcms.Models;
using System.Text.RegularExpressions;

namespace iGoo.Areas.Webcms.Controllers
{
    public class UserController : DefaultController
    {
        private String per = Libs.GetPermission("USER");

        public ActionResult Index()
        {
            LoadDefault();
            
            //if (per.IndexOf("S") < 0)
            //    return View("NotPermission")

            //Slect group
            AttributeViewModel at = new AttributeViewModel();
            at.Code = "ATTRIBUTE_GROUP_USER";
            ViewBag.GroupName = at.SelectChild().AsEnumerable().ToList();

            //Select user
            UserViewModel uv = new UserViewModel();
            if (!Request.IsNull("txtKey"))
                uv.UserName = Request.Get("txtKey");
            if (!Request.IsNull("slSearchGroup"))
                uv.GroupID = new Guid(Request.Get("slSearchGroup"));
            if (!Request.IsNull("slSearchStatus"))
                uv.Status = Request.GetNumber("slSearchStatus");
            if (!Request.IsNull("slSearchGender"))
                uv.Gender = Request.GetNumber("slSearchGender");

            uv.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            uv.PageSize = Request.IsNull("show") ? 20 : Request.GetNumber("show");

            if (!Request.IsNull("OrderBy") && !Request.IsNull("Order"))
                uv.OrderBy = Request.Get("OrderBy") + " " + Request.Get("Order");

            //Eidt
            if (!Request.IsNull("ID"))
            {
                uv.UserID = new Guid(Request.Get("ID"));
                ViewBag.Edit = uv.SelectOne().AsEnumerable().ToList();
            }
            //select inventory
            
            InventoryViewModel inv = new InventoryViewModel();
            inv.UserID = uv.UserID;
            ViewBag.Inventory = inv.SelectUserMenu().AsEnumerable().ToList();

            //Page
            List<DataRow> list = uv.SelectAll().AsEnumerable().ToList();
            ViewBag.User = list;
            ViewBag.TotalPages = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"]) / (double)uv.PageSize) : 0;
            return View();
        }

        [HttpPost]
        public ActionResult Create()
        {
            try
            {
                UserViewModel u = new UserViewModel();
                u.FullName = Request.Get("txtFullName");
                u.Email = Request.Get("txtEmail");
                u.Address = Request.Get("txtAddress");
                u.Info = Request.Get("txtInfo");
                u.GoogleID = Request.Get("txtGoogleID");
                u.Image = Request.Get("txtImage");
                u.Status = Request.GetNumber("slStatus");
                u.Gender = Request.GetNumber("slGender");
                u.GroupID = new Guid(Request.Get("slGroupID"));
                u.UserName = Request.Get("txtUserName");
                u.InventoryID = new Guid(Request.Get("slOrderInv")); 
                if (!"".Equals(u.GoogleID))
                {
                    String pt = @"width=""200"" height=""200"" src=""(?<Url>.*?)"" />";
                    Regex rg = new Regex(pt, RegexOptions.Singleline);
                    Match m = rg.Match(Libs.GetHtml("https://plus.google.com/" + u.GoogleID.ToString() + "/posts"));
                    u.Image = m.Groups["Url"].Value;
                }
                if (Request.IsNull("ID"))
                {
                    if (per.IndexOf("I") < 0)
                        return View("NotPermission");
                    u.Password = Libs.sMD5(Request.Get("txtPassword"));
                    u.Created = DateTime.Now;
                    u.UserID = Guid.NewGuid();
                    u.Insert();
                    SaveUserLog(UserForm.User.ToString(), UserActionType.Insert.ToString(), "Create new user: " + u.UserName.ToString());
                }
                else
                {
                    if (per.IndexOf("U") < 0)
                        return View("NotPermission");
                    u.UserID = new Guid(Request.Get("ID"));
                    if (!Request.Get("txtPassword").Equals("password") && !Request.IsNull("txtPassword"))
                        u.Password = Libs.sMD5(Request.Get("txtPassword"));
                    u.Update();
                    SaveUserLog(UserForm.User.ToString(), UserActionType.Update.ToString(), "Update user: " + u.UserName.ToString());
                }

                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/User" + returnUrl + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/User?error=1");
            }
        }

        [HttpPost]
        public ActionResult Update()
        {
            try
            {
                if (per.IndexOf("U") < 0)
                    return View("NotPermission");
                UserViewModel u = new UserViewModel();
                for (int i = 1; i <= Request.GetNumber("count"); i++)
                {
                    if (!Request.IsNull("ckID-" + i.ToString()))
                    {
                        u.UserID = new Guid(Request.Get("ckID-" + i.ToString()));
                        u.Status = (Request.GetNumber("slStatus-" + i.ToString()));
                        u.Update();
                        SaveUserLog(UserForm.User.ToString(), UserActionType.Update.ToString(), "Update user: " + u.UserName.ToString());
                    }
                }

                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/User" + returnUrl + "&result=1");
            }
            catch(Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/User?error=1");
            }
        }

        [HttpPost]
        public ActionResult Delete()
        {
            try
            {
                if (per.IndexOf("D") < 0)
                    return View("NotPermission");
                UserViewModel u = new UserViewModel();
                for (int i = 1; i <= Request.GetNumber("count"); i++)
                {
                    if (!Request.IsNull("ckID-" + i.ToString()))
                    {
                        u.UserID = new Guid(Request.Get("ckID-" + i.ToString()));
                        var deleteUser = u.SelectOne();
                        u.Delete();
                        SaveUserLog(UserForm.User.ToString(), UserActionType.Delete.ToString(), "Delete user: " + deleteUser.Rows[0]["UserName"]);
                    }
                }

                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/User" + returnUrl + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/User?error=1");
            }
        }

        public ActionResult Permission()
        {
            if (per.IndexOf("P") < 0)
                return View("NotPermission");
            Libs.CheckUser();

            ///Select default
            //Select user
            ViewBag.LoginUserID = Session["UserID"];
            ViewBag.LoginFullName = Session["FullName"];

            //Select menu
            ModuleViewModel mv = new ModuleViewModel();
            mv.MenuAll = mv.SelectAll();
            ViewBag.Menu = mv.SelectMenu(Guid.Empty);
            //End select default

            UserViewModel uv = new UserViewModel();
            uv.UserID = new Guid(Request.Get("ID"));
            ViewBag.Roll = uv.SelectRollUsersByUserID().AsEnumerable().ToList();
            
            InventoryViewModel iv = new InventoryViewModel();
            iv.UserID = new SqlGuid(Request.Get("ID"));
            List<DataRow> list = iv.SelectAll1().AsEnumerable().ToList();
            ViewBag.Inventory = list;

            InventoryViewModel ivde = new InventoryViewModel();
            ivde.UserID = new SqlGuid(Request.Get("ID"));
            //List<DataRow> listde = ivde.SelectInvDef().AsEnumerable().ToList();
            List<DataRow> listde = ivde.SelectAll1().AsEnumerable().ToList();
            ViewBag.InventoryDefault = listde;

            return View();
        }

        [HttpPost]
        public ActionResult UpdatePermission()
        {
            try
            {
                if (per.IndexOf("P") < 0)
                    return View("NotPermission");
                Guid userID = new Guid(Request.Get("UserID"));
                UserViewModel uv = new UserViewModel();
                uv.UserID = userID;
                var list = uv.SelectRollUsersByUserID().AsEnumerable();
                clsADM_RollUsers ru = new clsADM_RollUsers();
                ru.UserID = userID;
                var u = uv.SelectOne();

                String[] id = Request.Get("hID").Split(',');

                int count;
                for (int i = 0; i < id.Length; i++)
                {
                    RollViewModel rv = new RollViewModel();
                    count = list.Where(a => a["Checked"].ToString() == id[i]).Count();
                    if (Request.IsNull("ckID-" + id[i]))
                    {
                        if (count > 0)
                        {
                            uv.RollID = new Guid(id[i]);
                            rv.RollID = uv.RollID;
                            var r = rv.SelectRollModulesByRollID();
                            uv.DeleteRollUsers();
                            SaveUserLog(UserForm.User.ToString(), UserActionType.Delete.ToString(), "Delete user role: UserName: " + u.Rows[0]["UserName"] + " -> role: " + r.Rows[0]["Name"]);
                        }
                    }
                    else
                    {
                        if (count == 0)
                        {
                            ru.RollID = new Guid(id[i]);
                            ru.Insert();
                        }
                    }
                }

                String[] id1 = Request.Get("hID1").Split(',');
                uv.DeleteInvUsers();
                for (int i = 0; i < id1.Length; i++)
                {
                    int j = i + 1;
                    if (!Request.IsNull("ckID1-" + j))
                    {
                        uv.InventoryID = new Guid(id1[i]);
                        //if (uv.InventoryID.ToString() == Request.Get("slOrderInv").ToString())
                        //{
                        //    uv.Default = 1;
                        //}
                        uv.InsertInvUser();
                    }
                }

                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/User" + returnUrl + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/User?error=1");
            }
        }
    }
}

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
using System.Reflection;

namespace iGoo.Areas.Webcms.Controllers
{
    public class LogController : DefaultController
    {
        private String per = Libs.GetPermission("MEMBER");
        public ActionResult Index()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            ViewBag.UserActionType = Enum.GetNames(typeof(UserActionType));
            //ViewBag.sUserForm = Enum.GetNames(typeof(UserForm));            
            List<string> messages = new List<string>();
            foreach (FieldInfo field in typeof(UserForm).GetFields())
            {
                messages.Add(field.GetRawConstantValue().ToString());
            }
            ViewBag.sUserForm = messages;
            return View();
        }

        public string GetUserLog(string pageIndex = "1", string pageSize = "1000", string userName = null, string fromDate = null, string toDate = null, string actionType = null, string form = null)
        {
            LoadDefault();

            if (per.IndexOf("S") < 0)
                return string.Empty;

            LogViewModel lv = new LogViewModel();
            lv.PageIndex = int.Parse(pageIndex);
            lv.PageSize = int.Parse(pageSize);
            lv.OrderBy = "Created";
            lv.UserName = string.IsNullOrEmpty(userName) ? null : userName;
            lv.FromDate = string.IsNullOrEmpty(fromDate) ? null : fromDate;
            lv.ToDate = string.IsNullOrEmpty(toDate) ? null : toDate;
            lv.ActionType = string.IsNullOrEmpty(actionType) ? null : actionType;
            lv.Form = string.IsNullOrEmpty(form) ? null : form;

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;
            DataTable dt = lv.SelectAll();
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
    }
}

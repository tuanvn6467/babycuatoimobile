using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
    public class ShipperController : DefaultController
    {
        //
        // GET: /Webcms/Shipper/
        private String per = Libs.GetPermission("SHIPPER");
        public ActionResult Index()
        {
            LoadDefault();

            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            string strUserId = (string)Session["UserID"];
            CategoryViewModel ct = new CategoryViewModel();
            ct.Code = "CATEGORY_PRODUCT";
            ViewBag.GroupProduct = ct.SelectChild().AsEnumerable().ToList();

            AttributeViewModel at = new AttributeViewModel();
            at.Code = "ATTRIBUTE_PRODUCT_STATUS";
            ViewBag.GroupType = at.SelectChild().AsEnumerable().ToList();

//            InventoryViewModel iv = new InventoryViewModel();
//            ViewBag.Inventory = iv.SelectOptimize().AsEnumerable().ToList();
            //select inventory
            InventoryViewModel inv = new InventoryViewModel();
            inv.UserID = new SqlGuid(strUserId);
            ViewBag.Inventory = inv.SelectUserMenu().AsEnumerable().ToList();

            ShipperViewModel sv = new ShipperViewModel();
            ViewBag.Shipper = sv.SelectOptimize().AsEnumerable().ToList();

            ViewBag.Province = sv.SelectProvince().AsEnumerable().ToList();


            //Select news
            OrderViewModel ov = new OrderViewModel();
            if (!Request.IsNull("txtKey"))
                ov.OrderCode = Request.Get("txtKey");
            if (!Request.IsNull("slSearchProvince"))
                ov.ProvinceID = Request.GetNumber("slSearchProvince");

            if (!Request.IsNull("District"))
                ov.DistrictID = Request.GetNumber("District");

            if (!Request.IsNull("slsShipper"))
                ov.ShipperID = new Guid(Request.Get("slsShipper"));

            if (!Request.IsNull("txtFromDate"))
                ov.FromDate = Request.Get("txtFromDate");
            //else
            //    ov.FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            if (!Request.IsNull("txtToDate"))
                ov.ToDate = Request.Get("txtToDate");

            if (!Request.IsNull("slSearchStatus"))
                ov.Status = Request.GetNumber("slSearchStatus");

            ov.Status = Request.IsNull("slSearchStatus") ? ov.Status = 3 : ov.Status = Request.GetNumber("slSearchStatus");

            if (ov.Status == 5)
            {
                ViewBag.TitleStatus = "Trạng thái";
            }
            else
                ViewBag.TitleStatus = "";
            ov.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            ov.PageSize = Request.IsNull("show") ? 20 : Request.GetNumber("show");


            if (!Request.IsNull("OrderBy") && !Request.IsNull("Order"))
                ov.OrderBy = Request.Get("OrderBy") + " " + Request.Get("Order");

            //Eidt
            if (!Request.IsNull("ID"))
            {
                ov.OrderID = new Guid(Request.Get("ID"));
                ViewBag.Edit = ov.SelectOne().AsEnumerable().ToList();
            }

                        //<option value="0">Chờ xử lý</option>
                        //<option value="1">Xác nhận</option>
                        //<option value="2">Hoàn tất</option>
                        //<option value="3">Hoàn lại</option>
                        //<option value="4">Huỷ bỏ</option>
            // --> 1: chua giao hang, 2,3,4 Nhap lai

            //Page
            ov.UserID = new SqlGuid(strUserId);
            List<DataRow> list = ov.SelectOptimize().AsEnumerable().ToList();
            ViewBag.Order = list;
            ViewBag.TotalPages = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"]) / (double)ov.PageSize) : 0;
            return View();
        }



        [HttpPost]
        public ActionResult Update()
        {
            try
            {
                if (per.IndexOf("U") < 0)
                    return View("NotPermission");
                OrderViewModel ov = new OrderViewModel();
                int lstatus;
                for (int i = 1; i <= Request.GetNumber("count"); i++)
                {
                    if (!Request.IsNull("ckID-" + i.ToString()))
                    {
                        ov.OrderID = new Guid(Request.Get("ckID-" + i.ToString()));
                        ov.UserDelivery = new Guid(Session["UserID"].ToString());
                        lstatus = Request.GetNumber("hdfStatus-" + i.ToString());
                        if (lstatus == 1 || lstatus == 3) // tt xac nhan or hoan lai
                        {

                                ov.InventoryID = new Guid(Request.Get("hdfInventoryID-" + i.ToString()));
                                ov.ShipperID = new Guid(Request.Get("slShipper-" + i.ToString()));
                                ov.Request = Request.Get("txtRequest-" + i.ToString());
                                
                                ov.ProcessShip();
                        }
                        if (lstatus==6) // tt dang chuyen hang --> cap nhat lai
                        {
                            ov.Status = Request.GetNumber("slStatus-" + i.ToString());
                            ov.Request = Request.Get("txtRequest-" + i.ToString());
                            ov.ProcessUpdate();
                        }
                        
                    }
                }
                string returnUrl = Request.Get("returnUrl");
                return Redirect("/Webcms/Shipper" + returnUrl + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Shipper?error=1");
            }
        }

        public ActionResult GetDistrict(int ProvinceID)
	    {
            ShipperViewModel sv = new ShipperViewModel();
            List <SelectListItem > districts = new List<SelectListItem >();

            DataTable dt = sv.SelectDistrictByProvinceID(ProvinceID);
            foreach (DataRow row in dt.Rows)
            {
                districts.Add(new SelectListItem() 
                {
                    Text = row["name"].ToString(), 
                    Value = row["districtid"].ToString()
                });
            }
            return Json(districts);
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

        public string CheckOrderStatus(string orderCode) //JsonResult
        {
            string resultStr = "";
            OrderViewModel od = new OrderViewModel();
            od.OrderCode = orderCode;
            List<DataRow> list = od.SelectOrderStatus().AsEnumerable().ToList();
            foreach (DataRow dataRow in list)
            {
                resultStr = dataRow["Status"].ToString();
            }
            return resultStr;
        }
    }
}

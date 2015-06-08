using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using iGoo.Areas.Webcms.Models;
using iGoo.Helpers;
using System.Data;
using iGoo.Classes;

namespace iGoo.Areas.Webcms.Controllers
{
    public class OrderController : DefaultController
    {
        private String per = Libs.GetPermission("ORDER");
        public ActionResult Index()
        {
            LoadDefault();

            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            //Select group
            string strUserId = (string)Session["UserID"];
            CategoryViewModel ct = new CategoryViewModel();
            ct.Code = "CATEGORY_PRODUCT";
            ViewBag.GroupProduct = ct.SelectChild().AsEnumerable().ToList();

            AttributeViewModel at = new AttributeViewModel();
            at.Code = "ATTRIBUTE_PRODUCT_STATUS";
            ViewBag.GroupType = at.SelectChild().AsEnumerable().ToList();
            //Select news
            OrderViewModel ov = new OrderViewModel();
            ov.SaleID = new SqlGuid(strUserId);
            
            ov.OType = 2;
            if (!Request.IsNull("t"))
                ov.OType = Request.GetNumber("t");
            if (ov.OType > 1)
            {
                ViewBag.OStatus =
                    "<option value='0'>Chờ xử lý</option><option value='1'>Đang xử lý</option><option value='5'>Chờ vào sổ</option><option value='3'>Chờ giao hàng</option><option value='6'>Đang giao hàng</option><option value='2'>Hoàn tất</option><option value='4'>Huỷ bỏ</option>";
            }
            else
            {
                ViewBag.OStatus = "<option value='2'>Hoàn tất</option>";
            }
            ViewBag.oType = ov.OType;
            ov.ProvinceID = Request.GetNumber("province");
            ov.DistrictID = Request.GetNumber("district");
            ViewBag.Province = ov.SelectMenuProvince().AsEnumerable().ToList();
            ViewBag.District = ov.SelectMenuDistrict().AsEnumerable().ToList();
            if (!Request.IsNull("txtKey"))
                ov.OrderCode = Request.Get("txtKey");
            if (!Request.IsNull("slSearchStatus"))
                ov.Status = Request.GetNumber("slSearchStatus");
            else
                ov.Status = 0;
            if(ov.OType == 1)
                ov.Status = 2;
            ov.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            ov.PageSize = Request.IsNull("show") ? 20 : Request.GetNumber("show");


            if (!Request.IsNull("OrderBy") && !Request.IsNull("Order"))
                ov.OrderBy = Request.Get("OrderBy") + " " + Request.Get("Order");

            //Edit
            if (!Request.IsNull("ID"))
            {
                ov.OrderID = new Guid(Request.Get("ID"));
                ViewBag.Edit = ov.SelectOne().AsEnumerable().ToList();
            }

            //Page
            List<DataRow> list = ov.SelectAll().AsEnumerable().ToList();
            ViewBag.Order = list;
            ViewBag.TotalPages = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"]) / (double)ov.PageSize) : 0;
            return View();
        }

        public ActionResult ProcessOrder()
        {
            LoadDefault();

            if (per.IndexOf("S") < 0)
                return View("NotPermission");

            string strUserId = (string)Session["UserID"];            
            
            UserViewModel uv = new UserViewModel();
            uv.UserID = new SqlGuid(strUserId); 
            ViewBag.Roll = uv.SelectRollUsersByUserID().AsEnumerable().ToList();            

            int oType = Request.GetNumber("t");
            string oStatus = "";
            string strAction = "Thêm";
            if (oType == 1)
            {
                oStatus = "<option value='2'>Hoàn tất</option>";
                strAction = "Hoàn tất";
            }
            else
                oStatus = "<option value='1'>Đang xử lý</option><option value='5'>Chờ vào sổ</option>";
            ViewBag.oType = oType;
            AttributeViewModel at = new AttributeViewModel();
            at.Code = "ATTRIBUTE_PAYMENT";
            ViewBag.Payment = at.SelectChild().AsEnumerable().ToList();
            at.Code = "ATTRIBUTE_PRODUCT_STATUS";
            ViewBag.GroupType = at.SelectChild().AsEnumerable().ToList();
            CategoryViewModel ct = new CategoryViewModel();
            //ViewBag.GroupProduct = ct.SelectChild().AsEnumerable().ToList();
            ct.MenuAll = ct.SelectOptimize();
            ViewBag.MenuCate = ct.SelectMenu(new Guid("87A2AADF-D5F7-455D-AF80-71ECC2B683AC"));
            
            OrderDetailViewModel odv = new OrderDetailViewModel();
            OrderViewModel ov = new OrderViewModel();
            string btnAction = "btnActionAdd";
            
            //select inventory
            InventoryViewModel inv = new InventoryViewModel();
            inv.UserID = new SqlGuid(strUserId);
            ViewBag.MenuInv = inv.SelectUserMenu().AsEnumerable().ToList();
            
            string strDisable = "style=visibility:hidden;";
            string strDisable1 = "style=visibility:hidden;";
            ViewBag.Province = ov.SelectMenuProvince().AsEnumerable().ToList();
            ov.ProvinceID = Request.GetNumber("province");
            ViewBag.District = ov.SelectMenuDistrict().AsEnumerable().ToList();
            ViewBag.UserId = strUserId;
            ViewBag.CusClass = ov.SelectMenuCusClass().AsEnumerable().ToList();
            if (!Request.IsNull("ID"))
            {
                btnAction = "btnActionUpdate";
                strAction = "Cập nhật";
                ov.OrderID = new Guid(Request.Get("ID"));
                List<DataRow> e = ov.SelectOne().AsEnumerable().ToList();
                ViewBag.Edit = e;
                if (e[0]["Status"].ToString().Equals("0"))//Cho XL
                {
                    oStatus = "<option value='1'>Đang xử lý</option><option value='5'>Chờ vào sổ</option><option value='4'>Huỷ bỏ</option>";
                }
                else if (e[0]["Status"].ToString().Equals("1"))//Dang XL
                {
                    oStatus = "<option value='5'>Chờ vào sổ</option><option value='4'>Huỷ bỏ</option>";
                }
                else if (e[0]["Status"].ToString().Equals("3"))//Cho giao hang
                {
                    //oStatus = "<option value='6'>Đang giao hàng</option><option value='4'>Huỷ bỏ</option>";
                    //Khanh sua
                    oStatus = "<option value='3'>Chờ giao hàng</option><option value='5'>Chờ vào sổ</option>";
                }
                else if (e[0]["Status"].ToString().Equals("4"))//Huy
                {
                    oStatus = "<option value='4'>Huỷ bỏ</option>";
                }
                else if (e[0]["Status"].ToString().Equals("6"))//Dang giao hang
                {
                    //oStatus = "<option value='2'>Hoàn tất</option><option value='4'>Huỷ bỏ</option>";
                    //Khanh sua
                    oStatus = "<option value='6'>Đang giao hàng</option>";
                }
                else if (e[0]["Status"].ToString().Equals("5"))//Cho vao so
                {
                    oStatus = "<option value='3'>Chờ giao hàng</option><option value='1'>Đang xử lý</option>";
                }
                else if (e[0]["Status"].ToString().Equals("2"))//Hoan tat
                {
                    oStatus = "<option value='2'>Hoàn tất</option>";
                }

                if (e[0]["Status"].ToString().Equals("2") || e[0]["Status"].ToString().Equals("3") || e[0]["Status"].ToString().Equals("4") || e[0]["Status"].ToString().Equals("5") || e[0]["Status"].ToString().Equals("6"))
                {
                    strDisable1 = " ";
                }

                odv.OrderID = new Guid(Request.Get("ID"));
                ViewBag.OrderDetails = odv.SelectOrderDetailByOrderID().AsEnumerable().ToList();
                strDisable = "";
                clsCMS_OrderVas cov = new clsCMS_OrderVas();
                cov.OrderId = ov.OrderID;
                ViewBag.OrderVas = cov.SelectByOrderId().AsEnumerable().ToList();
            }
            ViewBag.oStatus = oStatus;
            ViewBag.btnAction = btnAction;
            ViewBag.strAction = strAction;
            ViewBag.Disable = strDisable;
            ViewBag.Disable1 = strDisable1;
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
                OrderDetailViewModel odv = new OrderDetailViewModel();
                int oStatus = Request.GetNumber("orderStattus");
                if (oStatus == 2 || oStatus == 4 || oStatus == 6)
                {
                    for (int i = 1; i <= Request.GetNumber("count"); i++)
                    {
                        odv.OrderDetailID = new Guid(Request.Get("hID-" + i.ToString()));
                        odv.CancelOrder();
                    }
                }
                else
                {
                    ov.Status = Request.GetNumber("slStatus");
                    //ov.TotalPrice = 0;
                    for (int i = 1; i <= Request.GetNumber("count"); i++)
                    {
                        odv.Quantity = (Request.GetNumber("txtQuantity-" + i.ToString()));
                        odv.Price = (Request.GetDecimal("txtPrice-" + i.ToString()));
                        odv.Discount = (Request.GetNumber("txtDiscount-" + i.ToString()));
//                        if (odv.Discount<100)
//                            ov.TotalPrice += odv.Quantity * ((odv.Price * (100 - odv.Discount))/100);
//                        else
//                            ov.TotalPrice += odv.Quantity * (odv.Price - odv.Discount);
                        if (!Request.IsNull("ckID-" + i.ToString()))
                        {
                            odv.OrderDetailID = new Guid(Request.Get("ckID-" + i.ToString()));
                            odv.Update();
                        }
                    }
                    List<ShoppingCart> lstSC = getProducts(Request.Get("slOrderInv"), Request.Get("cboCusClass"));
                    foreach (ShoppingCart sc in lstSC)
                    {
                        odv.OrderDetailID = Guid.NewGuid();
                        odv.OrderID = new Guid(Request.Get("OrderID"));
                        odv.ProductID = new Guid(sc.ProductID);
                        odv.Price = sc.SalePrice;
                        odv.Quantity = sc.Quantity;
                        odv.Discount = sc.Discount;
                        odv.ProductType = sc.Attribute;
                        odv.Created = DateTime.Now;
                        //ov.TotalPrice += odv.Quantity * ((odv.Price * (100 - odv.Discount)) / 100);
                        odv.Insert();
                    }
                    List<OrderVas> lstOV = getListVas();
                    clsCMS_OrderVas cov = new clsCMS_OrderVas();
                    foreach (OrderVas sc in lstOV)
                    {
                        cov.ID = Guid.NewGuid();
                        cov.OrderId = new Guid(Request.Get("OrderID"));
                        cov.VasId = new Guid(sc.VasId);
                        cov.Price = sc.Price;
                        cov.Insert();
                    }
                    List<OrderVas> lstCurOV = getListCurVas();
                    foreach (OrderVas sc in lstCurOV)
                    {
                        cov.OrderId = new Guid(Request.Get("OrderID"));
                        cov.VasId = new Guid(sc.VasId);
                        cov.Price = sc.Price;
                        cov.Update();
                    }
                }
                ov.Address = Request.Get("txtAddressDec");
                ov.FullName = Request.Get("txtFullnameDec");
                ov.Email = Request.Get("txtEmailDec");
                ov.Phone = Request.Get("txtPhoneDec");
                ov.Request = Request.Get("txtRequest");
                ov.OrderID = new Guid(Request.Get("OrderID"));
                if (oStatus == 0 || oStatus == 1)
                {
                    ov.SaleID = new Guid(Session["UserID"].ToString());
                }
                else
                {
                    ov.SaleID = SqlGuid.Null;
                }
                ov.Comment = Request.Get("txtComment");
                ov.Tax = Request.GetNumber("txtTax");
                ov.TransportFee = Request.GetDecimal("txtTransportFee");
                ov.Status = Request.GetNumber("slStatus");
                ov.DistrictID = Request.GetNumber("district");
                ov.ProvinceID = Request.GetNumber("province");
                ov.CusClassID = new Guid(Request.Get("cboCusClass"));
                ov.InventoryID = new Guid(Request.Get("slOrderInv"));
                ov.Discount = Int32.Parse(Request.Get("txtDiscount").Replace(",", ""));
                ov.TotalPrice = Int32.Parse(Request.Get("orderCash1").Replace(",", ""));
                ov.OrderSend = DateTime.Now;
                ov.PrePayment = Int32.Parse(Request.Get("prePayment").Replace(",", "")); 
                ov.PageNumber = Request.GetNumber("txtPageNumber");
                ov.BookNumber = Request.Get("txtBookNumber");
                if (!Request.IsNull("slPayment"))
                    ov.PaymentID = new Guid(Request.Get("slPayment"));
                ov.UpdateOrderByOrderDetail();
                return Redirect("/Webcms/Order/ProcessOrder?ID=" + Request.Get("OrderID") + "&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Order/ProcessOrder?ID=" + Request.Get("OrderID") + "&error=1");
            }
        }

        [HttpPost]
        public ActionResult UpdateInventory()
        {
            try
            {
                if (per.IndexOf("U") < 0)
                    return View("NotPermission");
                OrderViewModel ov = new OrderViewModel();
                OrderDetailViewModel odv = new OrderDetailViewModel();
                int oStatus = Request.GetNumber("orderStattus");
                if (oStatus == 2 || oStatus == 4 || oStatus == 6)
                {
                    for (int i = 1; i <= Request.GetNumber("count"); i++)
                    {
                        odv.OrderDetailID = new Guid(Request.Get("hID-" + i.ToString()));
                        odv.CancelOrder();
                    }
                }
                else
                {
                    List<ShoppingCart> lstSC = getProducts(Request.Get("slOrderInv"), Request.Get("cboCusClass"));
                    foreach (ShoppingCart sc in lstSC)
                    {
                        odv.OrderDetailID = Guid.NewGuid();
                        odv.OrderID = new Guid(Request.Get("OrderID"));
                        odv.ProductID = new Guid(sc.ProductID);
                        odv.Price = sc.SalePrice;
                        odv.Quantity = sc.Quantity;
                        odv.Discount = sc.Discount;
                        odv.ProductType = sc.Attribute;
                        odv.Created = DateTime.Now;
                        odv.Insert();
                    }
                    ov.OrderID = new Guid(Request.Get("OrderID"));
                    ov.InventoryID = new Guid(Request.Get("slOrderInv"));
                    ov.CusClassID = new Guid(Request.Get("cboCusClass"));
                    ov.UpdateInventory();
                }
//                ov.Address = Request.Get("txtAddressDec");
//                ov.FullName = Request.Get("txtFullnameDec");
//                ov.Email = Request.Get("txtEmailDec");
//                ov.Phone = Request.Get("txtPhoneDec");
//                ov.Request = Request.Get("txtRequest");
//                ov.OrderID = new Guid(Request.Get("OrderID"));
//                if (oStatus == 0 || oStatus == 1)
//                {
//                    ov.SaleID = new Guid(Session["UserID"].ToString());
//                }
//                else
//                {
//                    ov.SaleID = SqlGuid.Null;
//                }
//                ov.Comment = Request.Get("txtComment");
//                ov.Tax = Request.GetNumber("txtTax");
//                ov.TransportFee = Request.GetDecimal("txtTransportFee");
//                ov.Status = Request.GetNumber("slStatus");
//                ov.DistrictID = Request.GetNumber("district");
//                ov.ProvinceID = Request.GetNumber("province");
//                ov.CusClassID = new Guid(Request.Get("cboCusClass"));
//                ov.InventoryID = new Guid(Request.Get("slOrderInv"));
//                ov.Discount = Int32.Parse(Request.Get("txtDiscount").Replace(",", ""));
//                ov.TotalPrice = Int32.Parse(Request.Get("orderCash1").Replace(",", ""));
//                ov.OrderSend = DateTime.Now;
//                ov.PrePayment = Int32.Parse(Request.Get("prePayment").Replace(",", ""));
//                ov.PageNumber = Request.GetNumber("txtPageNumber");
//                ov.BookNumber = Request.Get("txtBookNumber");
//                if (!Request.IsNull("slPayment"))
//                    ov.PaymentID = new Guid(Request.Get("slPayment"));
//                ov.UpdateOrderByOrderDetail();
                return Redirect("/Webcms/Order/ProcessOrder?ID=" + Request.Get("OrderID") + "&result=3");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Order/ProcessOrder?ID=" + Request.Get("OrderID") + "&error=3");
            }
        }

        [HttpPost]
        public ActionResult Delete()
        {
            try
            {
                if (per.IndexOf("D") < 0)
                    return View("NotPermission");
                OrderViewModel ov = new OrderViewModel();
                int count = Request.GetNumber("count");
                for (int i = 1; i <= count; i++)
                {
                    if (!Request.IsNull("ckID-" + i.ToString()))
                    {
                        ov.OrderID = new Guid(Request.Get("ckID-" + i.ToString()));
                        ov.Delete();
                        ov.DeleteByOrderId();
                    }
                }

                //OrderDetailViewModel odv = new OrderDetailViewModel();
                //for (int i = 1; i <= Request.GetNumber("count"); i++)
                //{
                //    if (!Request.IsNull("ckID-" + i.ToString()))
                //    {
                //        odv.OrderDetailID = new Guid(Request.Get("ckID-" + i.ToString()));
                //        odv.Delete();
                //    }
                //}

                //OrderViewModel ov = new OrderViewModel();
                //ov.OrderID = new Guid(Request.Get("ID"));
                //ov.SaleID = new Guid(Session["UserID"].ToString());
                //ov.Comment = Request.Get("txtComment");
                //ov.Tax = Request.GetNumber("txtTax");
                //ov.TransportFee = Request.GetDecimal("txtTransportFee");
                //ov.Discount = Request.GetDecimal("txtDiscount");
                //ov.Status = Request.GetNumber("slStatus");
                //ov.OrderSend = DateTime.Now;
                //ov.UpdateOrderByOrderDetail();
                int t = Request.GetNumber("t");
                return Redirect("/Webcms/Order?ID=" + Request.Get("OrderID") + "&result=1&t="+t);
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Order?error=1");
            }
        }
        
        [HttpPost]
        public bool DeleteOrderDetail()
        {
            try
            {
                OrderDetailViewModel odv = new OrderDetailViewModel();
                odv.OrderDetailID = new Guid(Request.Get("id"));
                odv.Delete();

                return true;
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return false;
            }
        }

        //sonln added 2013oct17;
        // 
        [HttpPost]
        public ActionResult AddNew()
        {
            string orderType = Request.Get("t");
            try
            {
                OrderViewModel ov = new OrderViewModel();
                List<OrderVas> lstOV = getNewVas();
                ov.OrderID = Guid.NewGuid();
                clsCMS_OrderVas cov = new clsCMS_OrderVas();
                foreach (OrderVas sc in lstOV)
                {
                    cov.ID = Guid.NewGuid();
                    System.Diagnostics.Debug.WriteLine("cov.ID: " + cov.ID);
                    cov.OrderId = ov.OrderID;
                    cov.VasId = new Guid(sc.VasId);
                    cov.Price = sc.Price;
                    cov.Insert();
                }
                if (!Request.IsNull("txtUserID"))
                    ov.SaleID = new Guid(Request.Get("txtUserID"));
                ov.OType = Request.GetNumber("t");
                ov.FullName = Request.Get("txtFullNameDec");
                ov.Email = Request.Get("txtEmailDec");
                ov.Address = Request.Get("txtAddressDec");
                ov.Phone = Request.Get("txtPhoneDec");
                ov.Request = Request.Get("txtRequest");
                ov.Tax = Request.GetNumber("txtTax");
                ov.TransportFee = Request.GetNumber("txtTransportFee");
                ov.Comment = Request.Get("txtComment");
                ov.Status = Request.GetNumber("slStatus");
                ov.DistrictID = Request.GetNumber("District");
                ov.ProvinceID = Request.GetNumber("Province");
                ov.CusClassID = new Guid(Request.Get("cboCusClass"));
                ov.InventoryID = new Guid(Request.Get("slOrderInv"));
                ov.Discount = Int32.Parse(Request.Get("txtDiscount").Replace(",", ""));
                ov.TotalPrice = Int32.Parse(Request.Get("orderCash1").Replace(",", ""));
                ov.PrePayment = Request.GetNumber("prePayment");
                if (!Request.IsNull("slPayment"))
                    ov.PaymentID = new Guid(Request.Get("slPayment"));
                ov.Created = DateTime.Now;
                ov.Insert();

                List<ShoppingCart> lstSC = getProducts(Request.Get("slOrderInv"), Request.Get("cboCusClass"));
                OrderDetailViewModel odv = new OrderDetailViewModel();
                InventoryDetailViewModel idv = new InventoryDetailViewModel();
                foreach (ShoppingCart sc in lstSC)
                {
                    odv.OrderDetailID = Guid.NewGuid();
                    odv.OrderID = ov.OrderID;
                    odv.ProductID = new Guid(sc.ProductID);
                    odv.Price = sc.SalePrice;
                    odv.Quantity = sc.Quantity;
                    odv.Discount = sc.Discount;
                    odv.ProductType = sc.Attribute;
                    odv.Created = DateTime.Now;
                    odv.Insert();
                    if (ov.Status == 2)
                    {
                        idv.ProductID = new Guid(sc.ProductID);
                        idv.ChangeType = 4;
                        idv.Kieu = 4;
                        idv.QuantityChange = sc.Quantity;
                        idv.InventoryID = new Guid(Request.Get("slOrderInv"));
                        idv.UserID = new Guid(Session["UserID"].ToString());
                        idv.BrokenQuantityChange = 0;
                        idv.Description = "Mua hàng trực tiếp";
                        idv.BrokenQuantity = 0;
                        idv.RefID = ov.OrderID;
                        DataTable dt = idv.SelectOne1();
                        idv.InventoryDetailID = new Guid(dt.Rows[0]["InventoryDetailID"].ToString());
                        idv.Change();
                    }
                }
                return Redirect("/Webcms/Order/ProcessOrder?ID="+ov.OrderID+"&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Order/ProcessOrder?t=" + orderType + "&error=1");
            }
        }

        public ActionResult GetProductJSON()
        {
            //Select product
            OrderViewModel ov = new OrderViewModel();
            ov.ProvinceID = Request.GetNumber("pId");

            string jsonString = GetJson(ov.SelectMenuDistrict());
            Response.Write(jsonString);
            return null;
        }

        public string GetJson(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;

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

        private List<ShoppingCart> getProducts(string invId, string cusId)
        {
            String value = Libs.ReadCookie("quick_shopping_cart");
            List<ShoppingCart> lstSC = new List<ShoppingCart>();

            if (!String.IsNullOrEmpty(value))
            {
                String[] row = Regex.Split(value, "\\$\\$");
                String id = "";
                for (int i = 1; i < row.Length; i++)
                {
                    ShoppingCart sc = new ShoppingCart();
                    String[] arr = row[i].Split('#');
                    sc.ProductID = arr[0];
                    sc.Quantity = Convert.ToInt16(arr[1]);
                    string discountval = Request.Get("txtDiscount-" + arr[0]);
                    sc.Discount = int.Parse(discountval);
                    lstSC.Add(sc);
                    id += arr[0] + ",";
                }
                if (id.IndexOf(',') > -1)
                    id = id.Substring(0, id.Length - 1);

                ProductViewModel pv = new ProductViewModel();
                if (!"".Equals(id))
                {
                    pv.Title = id;
                    foreach (DataRow row2 in pv.SelectProductInOrder(invId, cusId).Rows)
                    {
                        ShoppingCart sc2 = lstSC.Find(x => x.ProductID == row2["ProductID"].ToString());

                        if (sc2 != null)
                        {
                            sc2.Title = row2["Title"].ToString();
                            sc2.SEOName = row2["SEOName"].ToString();
                            sc2.Image = row2["Image"].ToString();
                            sc2.SalePrice = Convert.ToDecimal(row2["SalePrice"].ToString());
                            if (sc2.Discount < 100)
                            {
                                sc2.TotalPrice = ((sc2.SalePrice*(100-sc2.Discount))/100) * sc2.Quantity;
                            }
                            else
                            {
                                sc2.TotalPrice = (sc2.SalePrice  - sc2.Discount) * sc2.Quantity;
                            }
                        }
                    }
                }
            }
            return lstSC;
        }

        public string GetDistrict(string Id) //JsonResult
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;
            OrderViewModel ov = new OrderViewModel();
            int i = -1;
            //odv.ProvinceId = Request.GetNumber("provinceId");
            Int32.TryParse(Id, out i);
            ov.ProvinceID = i;
            DataTable dt = ov.SelectMenuDistrict();
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

        private List<OrderVas> getListVas()
        {
            String value = Libs.ReadCookie("quick_vas_cart");
            String curVas = Libs.ReadCookie("current_vas_cart");
            if(value!=null && !String.IsNullOrEmpty(curVas))
                value = value.Replace(curVas, "");
            List<OrderVas> listOV = new List<OrderVas>();

            if (!String.IsNullOrEmpty(value))
            {
                String[] row = Regex.Split(value, "\\$\\$");
                for (int i = 1; i < row.Length; i++)
                {
                    OrderVas ov = new OrderVas();
                    ov.VasId = row[i];
                    ov.Price = Convert.ToDecimal(Request.Get("txtVasCash-" + row[i]));
                    listOV.Add(ov);
                }
            }
            return listOV;
        }
        private List<OrderVas> getNewVas()
        {
            String value = Libs.ReadCookie("quick_vas_cart");
            List<OrderVas> listOV = new List<OrderVas>();

            if (!String.IsNullOrEmpty(value))
            {
                String[] row = Regex.Split(value, "\\$\\$");
                for (int i = 1; i < row.Length; i++)
                {
                    OrderVas ov = new OrderVas();
                    ov.VasId = row[i];
                    ov.Price = Convert.ToDecimal(Request.Get("txtVasCash-" + row[i]));
                    listOV.Add(ov);
                }
            }
            return listOV;
        }

        private List<OrderVas> getListCurVas()
        {
            String value = Libs.ReadCookie("current_vas_cart");
            List<OrderVas> listOV = new List<OrderVas>();
            if (!String.IsNullOrEmpty(value))
            {
                String[] row = Regex.Split(value, "\\$\\$");
                for (int i = 1; i < row.Length; i++)
                {
                    OrderVas ov = new OrderVas();
                    ov.VasId = row[i];
                    ov.Price = Convert.ToDecimal(Request.Get("txtVasCash-" + i));
                    listOV.Add(ov);
                }
            }
            return listOV;
        }

        [HttpPost]
        public bool DeleteOrderVas()
        {
            try
            {
                clsCMS_OrderVas odv = new clsCMS_OrderVas();
                odv.ID = new Guid(Request.Get("id"));
                odv.Delete();

                return true;
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return false;
            }
        }

        public ActionResult ListOrders()
        {
            LoadDefault();

            if (per.IndexOf("S") < 0)
                return View("NotPermission");
            string strUserId = (string)Session["UserID"];
            
            OrderViewModel ov = new OrderViewModel();
            ov.SaleID = new SqlGuid(strUserId);           
            
            if (!Request.IsNull("txtKey"))
                ov.OrderCode = Request.Get("txtKey");
            ViewBag.OrderCount = ov.SelectOrderCount().AsEnumerable().ToList();
            if (!Request.IsNull("slSearchStatus"))
                ov.Status = Request.GetNumber("slSearchStatus");
            else
                ov.Status = 0;
            ViewBag.ViewStatus = ov.Status;
            ov.PageIndex = Request.IsNull("page") ? 1 : Request.GetNumber("page");
            ov.PageSize = Request.IsNull("show") ? 20 : Request.GetNumber("show");


            if (!Request.IsNull("OrderBy") && !Request.IsNull("Order"))
                ov.OrderBy = Request.Get("OrderBy") + " " + Request.Get("Order");

            //Edit
            if (!Request.IsNull("ID"))
            {
                ov.OrderID = new Guid(Request.Get("ID"));
                ViewBag.Edit = ov.SelectOne().AsEnumerable().ToList();
            }

            //Page
            List<DataRow> list = ov.SelectAll().AsEnumerable().ToList();
            ViewBag.Order = list;
            ViewBag.TotalPages = list.Count > 0 ? (int)Math.Ceiling(Convert.ToDouble(list[0]["TotalRows"]) / (double)ov.PageSize) : 0;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteList()
        {
            try
            {
                if (per.IndexOf("D") < 0)
                    return View("NotPermission");
                OrderViewModel ov = new OrderViewModel();
                int count = Request.GetNumber("count");
                for (int i = 1; i <= count; i++)
                {
                    if (!Request.IsNull("ckID-" + i.ToString()))
                    {
                        ov.OrderID = new Guid(Request.Get("ckID-" + i.ToString()));
                        ov.Delete();
                        ov.DeleteByOrderId();
                    }
                }
                return Redirect("/Webcms/Order/ListOrders?slSearchStatus=4&result=1");
            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                return Redirect("/Webcms/Order/ListOrders?slSearchStatus=4&error=1");
            }
        }
        //sonln ended
    }

    public class ShoppingCart
    {
        public String ProductID { get; set; }

        public String Attribute { get; set; }

        public int Quantity { get; set; }

        public int Discount { get; set; }

        public String Title { get; set; }

        public String SEOName { get; set; }

        public String Image { get; set; }

        public Decimal SalePrice { get; set; }

        public Decimal TotalPrice { get; set; }
    }

    public class OrderVas
    {
        public String VasId { get; set; }
        public Decimal Price { get; set; }
    }
}

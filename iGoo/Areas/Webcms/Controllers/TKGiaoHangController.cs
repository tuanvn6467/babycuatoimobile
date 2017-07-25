using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using iGoo.Areas.Webcms.Models;
using iGoo.Helpers;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using System.Data.SqlTypes;

namespace iGoo.Areas.Webcms.Controllers
{
    public class TKGiaoHangController : DefaultController
    {
        private String per = Libs.GetPermission("ORDER");

        private List<DataRow> list = new List<DataRow>();

        DTGiaoHangViewModel iv = new DTGiaoHangViewModel();
        UserViewModel sv = new UserViewModel();
        OrderViewModel ov = new OrderViewModel();
        int isFirstLoad = 0;
        
        public ActionResult Index()
        {
            LoadDefault();
            if (per.IndexOf("S") < 0)
                return View("NotPermission");

            //Slect group
            CategoryViewModel ct = new CategoryViewModel();
            ShipperViewModel sv = new ShipperViewModel();
            UserViewModel uv = new UserViewModel();
            string strUserId = (string)Session["UserID"];
            
            ct.MenuAll = ct.SelectOptimize();
            ViewBag.MenuCate = ct.SelectMenu(new Guid("87A2AADF-D5F7-455D-AF80-71ECC2B683AC"));

            AttributeViewModel at = new AttributeViewModel();
            at.Code = "ATTRIBUTE_MANUFACTURE";
            ViewBag.ManuFacture = at.SelectChild().AsEnumerable().ToList();
            at.Code = "ATTRIBUTE_PRODUCT_STATUS";
            ViewBag.Type = at.SelectChild().AsEnumerable().ToList();
            at.Code = "ATTRIBUTE_PRODUCT";
            ViewBag.Filter = at.SelectChild().AsEnumerable().ToList();

            ViewBag.NVGH = sv.SelectOptimize().AsEnumerable().ToList();
            ViewBag.NVBH = uv.SelectUserOptimize().AsEnumerable().ToList();

            ViewBag.CusClass = ov.SelectMenuCusClass().AsEnumerable().ToList();
            InventoryViewModel inv = new InventoryViewModel();
            inv.UserID = new SqlGuid(strUserId);
            List<DataRow> listIv = inv.SelectUserMenu2().AsEnumerable().ToList();
            ViewBag.Inventory = listIv;
            if (listIv.Count > 0)
            {
                ViewBag.InventoryId = listIv[0]["value"];
                iv.InventoryID = ViewBag.InventoryId;
            }
            ct.MenuAll = ct.SelectOptimize();
            bool hasParam = false;
            if (!Request.IsNull("slSearchInventory"))
            {
                iv.InventoryID = new Guid(Request.Get("slSearchInventory"));
                
            }
            if (!Request.IsNull("slsUser"))
            {
                iv.NVBH = new Guid(Request.Get("slsUser"));
                
            }
            if (!Request.IsNull("slsShipper"))
            {
                iv.NVGH = new Guid(Request.Get("slsShipper"));
                
            }
            if (!Request.IsNull("cboCusClass"))
            {
                iv.CusClassID = new Guid(Request.Get("cboCusClass"));

            }
            if (!Request.IsNull("txtTypeBuy"))
                iv.TypeBuy = Request.GetNumber("txtTypeBuy");
            if (!Request.IsNull("txtCompleteDate"))
            {
                iv.FromDate = Request.Get("txtCompleteDate");
                
            }
            if (!Request.IsNull("txtToDate"))
            {
                iv.ToDate = Request.Get("txtToDate");
            }
            if (!Request.IsNull("FirstLoad"))
                isFirstLoad = Request.GetNumber("FirstLoad");

            if(isFirstLoad == 1)
                list = iv.ReportTK_GiaoHang().AsEnumerable().ToList();           
            ViewBag.BaoCao = list;
            isFirstLoad = 1;
            ViewBag.FirstLoad = isFirstLoad;
            return View();
        }

        [HttpPost]
        public void ExportData()
        {
            try
            {
                if (!Request.IsNull("slSearchInventory"))
                    iv.InventoryID = new Guid(Request.Get("slSearchInventory"));
                if (!Request.IsNull("slsUser"))
                    iv.NVBH = new Guid(Request.Get("slsUser"));
                if (!Request.IsNull("slsShipper"))
                    iv.NVGH = new Guid(Request.Get("slsShipper"));
                if (!Request.IsNull("cboCusClass"))
                    iv.CusClassID = new Guid(Request.Get("cboCusClass"));
                if (!Request.IsNull("txtTypeBuy"))
                    iv.TypeBuy = Request.GetNumber("txtTypeBuy");
                if (!Request.IsNull("txtCompleteDate"))
                    iv.FromDate = Request.Get("txtCompleteDate");
                if (!Request.IsNull("txtToDate"))
                    iv.ToDate = Request.Get("txtToDate");

                list = iv.ReportTK_GiaoHang().AsEnumerable().ToList();
                var orderDetails = iv.ReportTK_GiaoHangChiTiet().AsEnumerable();
                int stt = 0;
                using (var wb = new XLWorkbook(XLEventTracking.Disabled))
                {
                    //XLWorkbook wb = new XLWorkbook(XLEventTracking.Disabled);
                    string sheetName = "ThongKe_DangGH"; //Give name for export file. 
                    var ws = wb.Worksheets.Add(sheetName);

                    # region header

                    ws.Cell(1,1).Value = "TT";
                    ws.Cell(1,2).Value = "Ngày giờ bán";
                    ws.Cell(1,3).Value = "Khách hàng";
                    ws.Cell(1,4).Value = "Mã hàng";
                    ws.Cell(1,5).Value = "Tên SP";
                    ws.Cell(1,6).Value = "SL";
                    ws.Cell(1,7).Value = "Đơn giá thực tế";
                    ws.Cell(1,8).Value = "Thành tiền";
                    ws.Cell(1,9).Value = "Tổng tiền";
                    //ws.Cell(1,10).Value = "Thanh toán trước";
                    //ws.Cell(1,11).Value = "Thanh toán tiền mặt";
                    ws.Cell(1,10).Value = "NV bán hàng";
                    //ws.Cell(1,13).Value = "Giao hàng";
                    ws.Cell(1,11).Value = "Loại KH";
                    ws.Cell(1,12).Value = "Chi nhánh";
                    ws.Cell(1,13).Value = "Ghi chú";
                    ws.Cell(1,1).Style.Font.Bold = true;
                    ws.Cell(1,2).Style.Font.Bold = true;
                    ws.Cell(1,3).Style.Font.Bold = true;
                    ws.Cell(1,4).Style.Font.Bold = true;
                    ws.Cell(1,5).Style.Font.Bold = true;
                    ws.Cell(1,6).Style.Font.Bold = true;
                    ws.Cell(1,7).Style.Font.Bold = true;
                    ws.Cell(1,8).Style.Font.Bold = true;
                    ws.Cell(1,9).Style.Font.Bold = true;
                    //ws.Cell(1,10).Style.Font.Bold = true;
                    //ws.Cell(1,11).Style.Font.Bold = true;
                    ws.Cell(1,10).Style.Font.Bold = true;
                    //ws.Cell(1,13).Style.Font.Bold = true;
                    ws.Cell(1,11).Style.Font.Bold = true;
                    ws.Cell(1,12).Style.Font.Bold = true;
                    ws.Cell(1,13).Style.Font.Bold = true;

                    # endregion

                    int n = 2;

                    #region set data 

                    for (int i = 0; i < list.Count; i++)
                    {
                        stt++;
                        var orderProductsList =
                            orderDetails.Where(
                                r => r.Field<Guid>("OrderID").CompareTo(new Guid(list[i]["OrderID"].ToString())) == 0)
                                .ToList();
                        int j = orderProductsList.Count;
                        ws.Cell(n, 1).Value = stt;
                        ws.Range(n, 1, n + j - 1, 2).Column(1).Merge();
                        ws.Cell(n, 2).Value = list[i]["OrderSend"] + " " + list[i]["OrderSendTime"];
                        ws.Range(n, 2, n + j - 1, 3).Column(1).Merge();
                        ws.Cell(n, 3).Value = list[i]["FullName"] + " - " + list[i]["Phone"] + " - " +
                                              list[i]["Address"];
                        ws.Range(n, 3, n + j - 1, 4).Column(1).Merge();
                        for (int k = 0; k < j; k++)
                        {
                            ws.Cell(n + k, 4).Value = orderProductsList[k]["SKU"];
                            ws.Cell(n + k, 5).Value = orderProductsList[k]["Title"];
                            ws.Cell(n + k, 6).Value = orderProductsList[k]["Quantity"];
                            ws.Cell(n + k, 7).Value = orderProductsList[k]["Price"];
                            ws.Cell(n + k, 8).Value = orderProductsList[k]["Cash"];
                        }
                        ws.Cell(n, 9).Value = list[i]["TotalPrice"];
                        ws.Range(n, 9, n + j - 1, 10).Column(1).Merge();
                        //ws.Cell(n, 10).Value = list[i]["PrePayment"];
                        //ws.Range(n, 10, n + j - 1, 11).Column(1).Merge();
                        //ws.Cell(n, 11).Value = list[i]["TienConLai"];
                        //ws.Range(n, 11, n + j - 1, 12).Column(1).Merge();
                        ws.Cell(n, 10).Value = list[i]["nhanvien"];
                        ws.Range(n, 10, n + j - 1, 11).Column(1).Merge();
                        //ws.Cell(n, 13).Value = list[i]["giaohang"];
                        //ws.Range(n, 13, n + j - 1, 14).Column(1).Merge();
                        ws.Cell(n, 11).Value = list[i]["CusClassName"];
                        ws.Range(n, 11, n + j - 1, 12).Column(1).Merge();
                        ws.Cell(n, 12).Value = list[i]["InventoryName"];
                        ws.Range(n, 12, n + j - 1, 13).Column(1).Merge();
                        ws.Cell(n, 13).Value = list[i]["Comment"];
                        ws.Range(n, 13, n + j - 1, 14).Column(1).Merge();
                        n += j;
                    }
                    #endregion

                    #region sum row
                    var sum1 = ws.Evaluate("SUM(I2:I" + (n - 1).ToString() + ")");
                    //var sum2 = ws.Evaluate("SUM(J2:J" + (n - 1).ToString() + ")");
                    //var sum3 = ws.Evaluate("SUM(K2:K" + (n - 1).ToString() + ")");
                    ws.Cell(n, 1).Value = "Cộng";
                    ws.Cell(n, 1).Style.Font.Bold = true;
                    ws.Range(n, 1, n + 1, 8).Row(1).Merge();
                    ws.Cell(n, 9).Value = sum1;
                    ws.Cell(n, 9).Style.Font.Bold = true;
                    //ws.Cell(n, 10).Value = sum2;
                    //ws.Cell(n, 10).Style.Font.Bold = true;
                    //ws.Cell(n, 11).Value = sum3;
                    //ws.Cell(n, 11).Style.Font.Bold = true;

                    #endregion


                    #region Style settings
                    ws.Columns().AdjustToContents();
                    ws.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.RangeUsed().Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    ws.Columns(7,10).Style.NumberFormat.Format = "#,###";
                    //ws.Cell(n, 10).Style.NumberFormat.Format = "#,###";
                    //ws.Cell(n, 11).Style.NumberFormat.Format = "#,###";
                    //ws.Cell(n, 12).Style.NumberFormat.Format = "#,###";
                    #endregion

                    HttpContext.Response.Clear();
                    HttpContext.Response.ContentType =
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContext.Response.AddHeader("content-disposition",
                        String.Format(@"attachment;filename={0}.xlsx", sheetName.Replace(" ", "_")));

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        wb.SaveAs(memoryStream);
                        memoryStream.WriteTo(HttpContext.Response.OutputStream);
                        memoryStream.Close();
                    }

                    HttpContext.Response.End();
                }


                //string returnUrl = Request.Get("returnUrl");
                //return Redirect("/Webcms/InventoryDetail" + returnUrl + "&result=1");

            }
            catch (Exception ex)
            {
                Libs.WriteError(ex.ToString());
                Redirect("/Webcms/BaoCao?error=1");
            }
            //return View("MyView"); 

        }
    }
}




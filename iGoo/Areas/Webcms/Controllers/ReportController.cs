//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//using System;
//using System.Data;
//using iGoo.Classes;
//using System.Data.SqlTypes;
//using System.Data.SqlClient;

//using System.Web.Mvc;
//using Microsoft.Reporting.WebForms;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iGoo.Areas.Webcms.Models;
using iGoo.Helpers;
using System.Data;
using iGoo.Classes;
using Microsoft.Reporting.WebForms;

namespace iGoo.Areas.Webcms.Controllers
{
    public class ReportsController : DefaultController
    {
        //
        // GET: /Webcms/Reports/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReportViewer2()
        {
            return View();
        }

        public ActionResult ReportViewer()
        {
            //ReportViewer rv = new Microsoft.Reporting.WebForms.ReportViewer();
            ////rv.ProcessingMode = ProcessingMode.Local;
            ////rv.LocalReport.ReportPath = Server.MapPath("~/Reports/Report1.rdlc");
            ////rv.LocalReport.Refresh();

            ////byte[] streamBytes = null;
            ////string mimeType = "";
            ////string encoding = "";
            ////string filenameExtension = "";
            ////string[] streamids = null;
            ////Warning[] warnings = null;

            ////streamBytes = rv.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

            ////return File(streamBytes, mimeType, "TestReport.pdf");


            ////
            //ReportViewModel ov = new ReportViewModel();
            ///* Put the stored procedure result into a dataset */
            //DataTable da = new DataTable();
            //da= ov.SelectByShipperID();
            //ReportDataSource datasource = new ReportDataSource("test report", da.DefaultView);

            //rv.ProcessingMode = ProcessingMode.Local;
            //rv.LocalReport.ReportPath = Server.MapPath("~/Reports/Report1.rdlc");           

            ////rv.LocalReport.DataSources.Clear();
            ////rv.LocalReport.DataSources.Add(datasource);
            ////rv.LocalReport.Refresh();

            ////Microsoft.Reporting.WebForms.ReportDataSource repds = new Microsoft.Reporting.WebForms.ReportDataSource("std_info", ds.Tables[0]);
            ////rv.ProcessingMode = ProcessingMode.Local;

            //rv.LocalReport.DataSources.Clear();

            //rv.LocalReport.DataSources.Add(datasource);

            //rv.Visible = true;

            ////rv.ReportRefresh();

            //// Report path where the .rdlc file exists, you have to mention it

            ////String rPath = @"c:\users\mohsinali\documents\visual studio 2010\Projects\reporting\reporting\Report1.rdlc";

            ////this.rv.LocalReport.ReportPath = "~/Reports/Report1.rdlc";

            ////this.rv.LocalReport.DataSources.Clear();

            ////this.rv.LocalReport.DataSources.Add(repds);

            ////this.rv.Visible = true;

            ////this.rv.ReportRefresh();
           
            return View();
        }

        public ActionResult ASPXView()
        {
            return View();
        }

        public ActionResult ASPXUserControl()
        {
            return View();
        }
        //public ActionResult ViewUserControl1()
        //{
        //    return View();
        //}
        public ActionResult Index1()
        {
            ReportViewModel ov = new ReportViewModel();
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Reports/Report1.rdlc") };
            var reportDataSource = new ReportDataSource("Customers", ov.SelectByShipperID());

            localReport.DataSources.Add(reportDataSource);
            const string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;
            const string deviceInfo = "<DeviceInfo>" +
                                      "  <OutputFormat>PDF</OutputFormat>" +
                                      "  <PageWidth>8.5in</PageWidth>" +
                                      "  <PageHeight>11in</PageHeight>" +
                                      "  <MarginTop>0.5in</MarginTop>" +
                                      "  <MarginLeft>1in</MarginLeft>" +
                                      "  <MarginRight>1in</MarginRight>" +
                                      "  <MarginBottom>0.5in</MarginBottom>" +
                                      "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;

            //Render the report
            byte[] renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);

        }
    }

        

    }


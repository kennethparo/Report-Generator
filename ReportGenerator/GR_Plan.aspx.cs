using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Net;


namespace ReportGenerator
{
    public partial class GR_Plan : System.Web.UI.Page
    {
        string filepath;

        protected void Page_Load(object sender, EventArgs e)
        {
            GRPlanReportViewer.ProcessingMode = ProcessingMode.Local;
            GRPlanReportViewer.LocalReport.ReportPath = Server.MapPath("~/GR_Plan.rdlc");
            GRPlanReport();
            GR_PDF();
        }

        private void GRPlanReport()
        {
            ReportDataSource GRPlan = new ReportDataSource("DataSet1", Session["PrintGRPLan"] as DataTable);
            GRPlanReportViewer.LocalReport.DataSources.Add(GRPlan);
        }

        private void GR_PDF()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = GRPlanReportViewer.LocalReport.Render(
               "PDF", null, out mimeType, out encoding,
                out extension,
               out streamids, out warnings);
            filepath = Server.MapPath("~/gr_plan.pdf");
            FileStream fs = new FileStream(filepath,
               FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();


            WebClient User = new WebClient();

            Byte[] FileBuffer = User.DownloadData(filepath);

            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }
        }
    }
}
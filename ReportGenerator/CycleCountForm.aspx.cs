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
    public partial class CycleCountForm : System.Web.UI.Page
    {
        string filepath;

        protected void Page_Load(object sender, EventArgs e)
        {
            CycleCountReportViewer.ProcessingMode = ProcessingMode.Local;
            CycleCountReportViewer.LocalReport.ReportPath = Server.MapPath("~/CycleCount.rdlc");
            CycleCountReport();
            CC_PDF();
        }

        public void CycleCountReport()
        {
            ReportDataSource CycleCount = new ReportDataSource("DataSet1", Session["PrintCycleCount"] as DataTable);
            CycleCountReportViewer.LocalReport.DataSources.Add(CycleCount);
        }

        private void CC_PDF()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = CycleCountReportViewer.LocalReport.Render(
               "PDF", null, out mimeType, out encoding,
                out extension,
               out streamids, out warnings);
            filepath = Server.MapPath("~/cyclecount.pdf");
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
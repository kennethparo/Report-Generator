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
    public partial class print_bintobintrancs : System.Web.UI.Page
    {
        string filepath;

        protected void Page_Load(object sender, EventArgs e)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/BintoBinTrans.rdlc");
            ReportTo();
            PDFnaEwan();
        }

        private void ReportTo()
        {
            ReportDataSource BintoBinTransac = new ReportDataSource("dataset_bintobintrans", Session["PRINTNAAAA"] as DataTable);
            ReportViewer1.LocalReport.DataSources.Add(BintoBinTransac);
        }

        private void PDFnaEwan()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = ReportViewer1.LocalReport.Render(
               "PDF", null, out mimeType, out encoding,
                out extension,
               out streamids, out warnings);
            filepath = Server.MapPath("~/sample.pdf");
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
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using com.msc.infraestructure.entities.mvc;
using com.msc.infraestructure.entities;
using System.Data;
using Microsoft.Reporting.WebForms;

namespace com.msc.infraestructure.utils
{
    public static class Printing
    {
        public static byte[] PrintPDF(DataTable tbl, string ds, string NombreReporte)
        {
            LocalReport report = new LocalReport();
            ReportDataSource rds = new ReportDataSource(ds, tbl);
            report.ReportPath = @"Impresiones\" + NombreReporte + ".rdlc";
            report.DataSources.Add(rds);

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            byte[] mybytes = report.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            return mybytes;
        }
    }
}
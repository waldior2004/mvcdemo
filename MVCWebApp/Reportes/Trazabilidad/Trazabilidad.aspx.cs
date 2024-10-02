using com.msc.infraestructure.utils;
using com.msc.services.interfaces;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace com.msc.frontend.mvc.Reportes.Trazabilidad
{
    public partial class Trazabilidad1 : System.Web.UI.Page
    {
        private ChannelFactory<IReporte> factrep;
        private IReporte proxyReporte;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    rptViewer.Visible = true;
                    rptViewer.LocalReport.DataSources.Clear();
                    rptViewer.LocalReport.ReportPath = @"Reportes\Trazabilidad\Trazabilidad.rdlc";
                    rptViewer.LocalReport.DisplayName = "Reporte de Trazabilidad";
                    var tblResult = Reporting.ToDataTable(GetProxyReporte().ObtTrazabilidad());
                    rptViewer.LocalReport.DataSources.Add(new ReportDataSource("dsTraza", tblResult));
                    rptViewer.LocalReport.Refresh();
                    rptViewer.DataBind();

                    CloseProxy();
                }
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
            }
        }

        private IReporte GetProxyReporte()
        {
            factrep = new ChannelFactory<IReporte>("Reporte");
            IReporte proxyReporte = factrep.CreateChannel();
            (proxyReporte as ICommunicationObject).Open();
            return proxyReporte;
        }

        private void CloseProxy()
        {
            if ((proxyReporte as ICommunicationObject).State == CommunicationState.Opened)
                (proxyReporte as ICommunicationObject).Close();
            else
                (proxyReporte as ICommunicationObject).Abort();
        }
    }
}
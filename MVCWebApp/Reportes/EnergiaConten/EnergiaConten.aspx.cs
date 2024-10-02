using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using com.msc.services.interfaces;
using System.ServiceModel;
using com.msc.infraestructure.utils;
using com.msc.infraestructure.entities.reportes;


namespace com.msc.frontend.mvc.Reportes.EnergiaConten
{
    public partial class EnergiaConten1 : System.Web.UI.Page
    {
        private ChannelFactory<IReporte> factrep;
        private IReporte proxyReporte;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    var IdNave = Request.QueryString["IdNave"].ToString();
                    var IdViaje = Request.QueryString["IdViaje"].ToString();
                    var FecIniApro = Request.QueryString["FecIniApro"].ToString();
                    var FecFinApro = Request.QueryString["FecFinApro"].ToString();
                    var FecIniZarpe = Request.QueryString["FecIniZarpe"].ToString();
                    var FecFinZarpe = Request.QueryString["FecFinZarpe"].ToString();
                    var IdPuerto = Request.QueryString["IdPuerto"].ToString();

                    var param = new pEnergiaConten
                    {
                        IdNave = IdNave,
                        IdViaje = IdViaje,
                        FecIniApro = FecIniApro,
                        FecFinApro = FecFinApro,
                        FecIniZarpe = FecIniZarpe,
                        FecFinZarpe = FecFinZarpe,
                        IdPuerto = IdPuerto
                    };

                    rptViewer.Visible = true;
                    rptViewer.LocalReport.DataSources.Clear();
                    rptViewer.LocalReport.ReportPath = @"Reportes\EnergiaConten\EnergiaConten.rdlc";
                    rptViewer.LocalReport.DisplayName = "Reporte de Energía de Contenedores Reefers";
                    var tblResult = Reporting.ToDataTable(GetProxyReporte().ObtEnergiaConten(param));
                    rptViewer.LocalReport.DataSources.Add(new ReportDataSource("EnergiaConten", tblResult));
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
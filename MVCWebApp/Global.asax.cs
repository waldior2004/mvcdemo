using com.msc.services.interfaces;
using System.ServiceModel;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace com.msc.frontend.mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private ChannelFactory<ISeguridad> factory;
        private ISeguridad proxyService;

        private ChannelFactory<ISistema> factsist;
        private ISistema proxySistema;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            factory = new ChannelFactory<ISeguridad>("Seguridad");
            ISeguridad proxyService = factory.CreateChannel();
            (proxyService as ICommunicationObject).Open();

            Application["proxySeguridad"] = proxyService;

            factsist = new ChannelFactory<ISistema>("Sistema");
            ISistema proxySistema = factsist.CreateChannel();
            (proxySistema as ICommunicationObject).Open();

            Application["proxySistema"] = proxySistema;

        }

        protected void Application_End()
        {
            if ((proxyService as ICommunicationObject).State == CommunicationState.Opened)
                (proxyService as ICommunicationObject).Close();
            else
                (proxyService as ICommunicationObject).Abort();

            if ((proxySistema as ICommunicationObject).State == CommunicationState.Opened)
                (proxySistema as ICommunicationObject).Close();
            else
                (proxySistema as ICommunicationObject).Abort();

        }
    }
}

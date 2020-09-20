using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Models.AutoMapper;
using SMSOnline.Filters;

namespace SMSOnline
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            AutoMapperConfig.Init();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalFilters.Filters.Add(new TrackLoginsFilter());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
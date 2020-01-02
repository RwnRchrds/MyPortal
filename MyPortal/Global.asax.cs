using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MyPortal.BusinessLogic.Services;

namespace MyPortal
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTA5NzE1QDMxMzcyZTMxMmUzMEpmWjBrYm9HNVRMYzEwTXZqUTFDS1I1MHc4TVRZQU5xK09JQmNwdDdmdjg9");

            MappingService.GetMapperBusinessConfiguration().ConfigurationProvider.AssertConfigurationIsValid();
            MappingService.GetMapperDataGridConfiguration().ConfigurationProvider.AssertConfigurationIsValid();
           GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
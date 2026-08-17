using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProductCatalog
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            // Log exception to Windows Event Log (legacy pattern)
            System.Diagnostics.EventLog.WriteEntry(
                "ProductCatalog",
                exception?.ToString() ?? "Unknown error",
                System.Diagnostics.EventLogEntryType.Error);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Session started
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Session ended
        }
    }
}

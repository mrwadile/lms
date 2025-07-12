using Library.Web.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Library.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // Register dependencies
            DependencyConfig.RegisterDependencies();
        }
        protected void Application_Error()
        {
            Exception ex = Server.GetLastError();

            string logDir = Server.MapPath("~/logs");
            string logFile = Path.Combine(logDir, "errors.txt");

            try
            {
                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }

                using (StreamWriter writer = new StreamWriter(logFile, true))
                {
                    writer.WriteLine("======== ERROR ========");
                    writer.WriteLine("Time: " + DateTime.Now);
                    writer.WriteLine("Message: " + ex.Message);
                    writer.WriteLine("Source: " + ex.Source);
                    writer.WriteLine("URL: " + HttpContext.Current?.Request?.Url);
                    writer.WriteLine("StackTrace: " + ex.StackTrace);
                    writer.WriteLine();
                }
            }
            catch
            {
            }
        }
        protected void Application_End()
        {
            // Clean up resources if necessary
            if (DependencyConfig.Container != null)
            {
                DependencyConfig.Container.Dispose();
            }
        }
    }
}

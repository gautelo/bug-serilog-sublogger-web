using System.IO;
using System.Web.Http;
using MyOrg.Logging;

namespace MyOrg.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var basePath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~") ?? "", "bin");
            // SerilogConfig.RegisterByCode(basePath);
            SerilogConfig.RegisterBySettings(basePath);
        }
    }
}

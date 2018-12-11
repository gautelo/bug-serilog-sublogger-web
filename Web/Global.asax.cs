using System.IO;
using System.Web.Http;
using MyOrg.Logging;
using Serilog;

namespace MyOrg.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public ILogger Logger { get; private set; }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var basePath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            var binPath = Path.Combine(basePath, "bin");
            var outputPath = Path.Combine(basePath, "Log");
            var configPath = Path.Combine(binPath, "Log.json");
            // SerilogConfig.RegisterByCode(outputPath);
            SerilogConfig.RegisterBySettings(outputPath, configPath);

            Logger = Log.Logger.ForContext<WebApiApplication>();
            Logger.Information(nameof(Application_Start));
        }

        protected void Application_End()
        {
            Logger.Information(nameof(Application_End));

            Log.CloseAndFlush();
        }
    }
}

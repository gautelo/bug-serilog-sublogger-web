using System.IO;
using System.Reflection;
using MyOrg.Logging;
using Serilog;

namespace MyOrg.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            // SerilogConfig.RegisterByCode(basePath);
            SerilogConfig.RegisterBySettings(basePath);

            var logger = Log.ForContext<Program>();

            logger.Information("Hello");
        }
    }
}

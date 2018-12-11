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
            var binPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var outputPath = Path.Combine(binPath, "..\\..\\Log\\");
            var configPath = Path.Combine(binPath, "Log.json");
            // SerilogConfig.RegisterByCode(outputPath);
            SerilogConfig.RegisterBySettings(outputPath, configPath);

            var logger = Log.ForContext<Program>();

            logger.Information("Hello");

            Log.CloseAndFlush();
        }
    }
}

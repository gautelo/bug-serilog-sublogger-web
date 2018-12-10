using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace MyOrg.Logging
{
    public static class SerilogConfig
    {
        public static void RegisterByCode(string basePath)
        {
            var logOutputPath = Path.Combine(basePath, "Log");

            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(Path.Combine(logOutputPath, "CodeLog-.log"), outputTemplate: "{Timestamp:HH:mm:ss.fff} {Level:u3} {ThreadId,3} {SourceContext} :> {Message}{NewLine}{Exception}", shared: true, flushToDiskInterval: TimeSpan.FromSeconds(3), rollingInterval: RollingInterval.Day)
                .WriteTo.Logger(lc => lc
                    .WriteTo.File(Path.Combine(logOutputPath, "CodeSub-.log"), outputTemplate: "{Timestamp:HH:mm:ss.fff} {Level:u3} {ThreadId,3} {SourceContext} :> {Message}{NewLine}{Exception}", shared: true, flushToDiskInterval: TimeSpan.FromSeconds(3), rollingInterval: RollingInterval.Day));

            Log.Logger = loggerConfiguration.CreateLogger();
        }

        public static void RegisterBySettings(string basePath)
        {
            var logOutputPath = Path.Combine(basePath, "Log");
            Environment.SetEnvironmentVariable("SERILOG_OUTPUT_PATH", logOutputPath);

            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(basePath, "Log.json")).Build();

            var loggerConfiguration = new LoggerConfiguration().ReadFrom.Configuration(configuration);

            Log.Logger = loggerConfiguration.CreateLogger();
        }
    }
}
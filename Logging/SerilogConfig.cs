using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace MyOrg.Logging
{
    public static class SerilogConfig
    {
        public static void RegisterByCode(string outputPath)
        {

            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(Path.Combine(outputPath, "CodeLog-.log"), outputTemplate: "{Timestamp:HH:mm:ss.fff} {Level:u3} {ThreadId,3} {SourceContext} :> {Message}{NewLine}{Exception}", shared: true, flushToDiskInterval: TimeSpan.FromSeconds(3), rollingInterval: RollingInterval.Day)
                .WriteTo.Logger(lc => lc
                    .WriteTo.File(Path.Combine(outputPath, "CodeSub-.log"), outputTemplate: "{Timestamp:HH:mm:ss.fff} {Level:u3} {ThreadId,3} {SourceContext} :> {Message}{NewLine}{Exception}", shared: true, flushToDiskInterval: TimeSpan.FromSeconds(3), rollingInterval: RollingInterval.Day));

            Log.Logger = loggerConfiguration.CreateLogger();
        }

        public static void RegisterBySettings(string outputPath, string configPath)
        {
            Environment.SetEnvironmentVariable("SERILOG_OUTPUT_PATH", outputPath);

            var configuration = new ConfigurationBuilder().AddJsonFile(configPath).Build();

            var loggerConfiguration = new LoggerConfiguration().ReadFrom.Configuration(configuration);

            Log.Logger = loggerConfiguration.CreateLogger();
        }
    }
}
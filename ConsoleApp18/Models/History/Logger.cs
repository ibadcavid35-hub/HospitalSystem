using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace ConsoleApp18.Models.History
{
    public class Logger
    {
        public static void SaveToCheck(string content)
        {
            var logger = Path.Combine(AppContext.BaseDirectory, "check", "checks.log");
            Directory.CreateDirectory(Path.GetDirectoryName(logger) ?? "");
            using var log = new LoggerConfiguration().WriteTo.File(logger, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            log.Information(content);
        }
    }
}

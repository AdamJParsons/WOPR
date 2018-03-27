using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOPR.Infrastructure.Utilities.Logging
{
    public class WOPRLogger : IWOPRLogger
    {
        private static NLog.Logger m_Logger = NLog.LogManager.GetCurrentClassLogger();

        public WOPRLogger()
        {
            Configure();
        }

        private void Configure()
        {
            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new NLog.Targets.FileTarget() { FileName = @"c:\Log\file.txt", Name = "logfile" };
            var logconsole = new NLog.Targets.ConsoleTarget() { Name = "logconsole" };

            config.LoggingRules.Add(new NLog.Config.LoggingRule("*", LogLevel.Info, logconsole));
            config.LoggingRules.Add(new NLog.Config.LoggingRule("*", LogLevel.Debug, logfile));

            NLog.LogManager.Configuration = config;
        }

        public void Log(string message)
        {
            m_Logger.Log(LogLevel.Info, message);
        }
    }

    public interface IWOPRLogger
    {
        void Log(string message);
    }
}

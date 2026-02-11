using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace MasterApplication.DB.Services
{
    public class MasterLogger
    {
        private static Logger _logger = NLogBuilder.ConfigureNLog($"nlog.config").GetCurrentClassLogger();
        public static void Info(string message, string componentName, string methodName)
        {
            _logger.Info(FormatLogEntry(message, componentName, methodName));
        }
        public static void Warn(string message, string componentName, string methodName)
        {
            _logger.Warn(FormatLogEntry(message, componentName, methodName));
        }
        public static void Error(System.Exception ex, string message = null)
        {
            _logger.Error(ex, message);
        }
        public static void Error(System.Exception ex, string message = null,long userid=0)
        {
            _logger.Error(ex, message,userid);
        }
        private static string FormatLogEntry(
                       string message,
                       string componentName, string methodName)
        {
            StringBuilder logEntry = new StringBuilder();
            logEntry.AppendFormat("ClassName={0} ", componentName);
            logEntry.AppendFormat("Method={0} ", methodName);
            logEntry.AppendFormat("Message={0}", message);
            logEntry.AppendLine();
            return logEntry.ToString();
        }
    }
}

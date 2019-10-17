using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Helpers
{
    public static class LogHelper
    {
        private static int _indentLevel = 0;
        private static readonly string _tabString = new string('\t', 26);

        public static void Begin(ILog log, string functionSig)
        {
            log.Debug($"{_tabString.Substring(0, _indentLevel)}Begin {functionSig}");
            if (_indentLevel < 25) _indentLevel++;
        }

        public static void Middle(ILog log, string message)
        {
            log.Debug($"{_tabString.Substring(0, _indentLevel)}{message}");
        }

        public static void End(ILog log, string functionSig)
        {
            if (_indentLevel > 0) _indentLevel--;
            log.Debug($"{_tabString.Substring(0, _indentLevel)}End {functionSig}");
        }

        public static void LogAll(ILog log, Exception ex)
        {
            Exception currentEx = ex;
            do
            {
                log.Error("Exception caught: ", currentEx);
                currentEx = currentEx.InnerException;
            } while (null != currentEx);
        }

        public static string GetAllErrorMessages(Exception ex)
        {
            Exception currentEx = ex;
            StringBuilder sb = new StringBuilder();
            do
            {
                sb.AppendLine(currentEx.ToString());
                currentEx = currentEx.InnerException;
            } while (null != currentEx);
            return sb.ToString();
        }

        public static void InitalizeLog4Net()
        {
            XmlConfigurator.Configure();
        }
    }
}

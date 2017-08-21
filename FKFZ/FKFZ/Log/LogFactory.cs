using log4net;
using System;
using System.IO;

namespace FKFZ.Log
{
    public class LogFactory
    {
        static LogFactory()
        {
            FileInfo configFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"Log.config");
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        public static LogImplement GetLogger(Type type)
        {
            return new LogImplement(LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));
        }

        public static LogImplement GetLogger(string str)
        {
            return new LogImplement(LogManager.GetLogger(str));
        }
    }
}

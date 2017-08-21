using System;

namespace FKFZ.Log
{
    public class RecordLog
    {
        public static void RecordException(Exception e)
        {
            LogImplement log = LogFactory.GetLogger(typeof(RecordLog));

            log.Error(e.Message + e.StackTrace);
        }

        public static void RecordWarning(Exception e)
        {
            LogImplement log = LogFactory.GetLogger(typeof(RecordLog));
            log.Warming(e.Message + e.StackTrace);
        }

        public static void RecordInfo(Exception e)
        {
            LogImplement log = LogFactory.GetLogger(typeof(RecordLog));
            log.Info(e.Message + e.StackTrace);
        }
        public static void RecordDebug(object message, Exception e)
        {
            LogImplement log = LogFactory.GetLogger(typeof(RecordLog));
            log.Debug(message,e);
        }
    }
}

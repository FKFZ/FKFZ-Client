using log4net;
using System;

namespace FKFZ.Log
{
    public class LogImplement
    {
        private ILog logger;

        public LogImplement(ILog log)
        {
            this.logger = log;
        }

        public void Debug(object message)
        {
            this.logger.Debug(message);
        }

        public void Debug(object message, Exception e)
        {
            this.logger.Debug(message, e);
        }

        public void Warming(object message)
        {
            this.logger.Warn(message);
        }

        public void Warming(object message, Exception e)
        {
            this.logger.Warn(message, e);
        }

        public void Error(object message)
        {
            this.logger.Error(message);
        }

        public void Error(object message, Exception e)
        {
            this.logger.Error(message, e);
        }

        public void Info(object message)
        {
            this.logger.Info(message);
        }

        public void Info(object message, Exception e)
        {
            this.logger.Info(message, e);
        }
    }
}

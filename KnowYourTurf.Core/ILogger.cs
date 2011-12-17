using System;
using log4net;

namespace KnowYourTurf.Core
{
    public interface ILogger
    {
        void LogDebug(string message, params object[] parameters);
        void LogDebug(string message);
        void LogInfo(string message, params object[] parameters);
        void LogInfo(string message);
        void LogWarn(string message, params object[] parameters);
        void LogWarn(string message);
        void LogError(string message, params object[] parameters);
        void LogError(string message);
        void LogFatal(string message, params object[] parameters);
        void LogFatal(string message);
        IDisposable Push(string context);
        void LogException(Exception exception);
    }

    public class NullLogger : ILogger, IDisposable
    {
        public void LogDebug(string message, params object[] parameters)
        {
        }

        public void LogDebug(string message)
        {

        }

        public void LogInfo(string message, params object[] parameters)
        {
        }

        public void LogInfo(string message)
        {

        }

        public void LogWarn(string message, params object[] parameters)
        {
        }

        public void LogWarn(string message)
        {

        }

        public void LogError(string message, params object[] parameters)
        {
        }

        public void LogError(string message)
        {

        }

        public void LogFatal(string message, params object[] parameters)
        {
        }

        public void LogFatal(string message)
        {

        }

        public IDisposable Push(string context)
        {
            return this;
        }

        public void LogException(Exception exception)
        {

        }

        public void Dispose() { }
    }

    public class ConsoleLogger : ILogger, IDisposable
    {
        private readonly string _type;
        private string _context;

        public ConsoleLogger(Type type)
        {
            _type = type.Name;
        }

        public void LogDebug(string message, params object[] parameters)
        {
            LogDebug(message.ToFormat(parameters));
        }

        public void LogDebug(string message)
        {
            writeLogToConsole("DEBUG", message);
        }

        private void writeLogToConsole(string level, string message)
        {
            if (_context != null)
            {
                Console.Write("context: {0}", _context);
            }

            Console.Write("{0} {1}: ", _type, level);

            if (message == null)
            {
                message = String.Empty;
            }

            Console.WriteLine(message);
        }

        public void LogInfo(string message, params object[] parameters)
        {
            LogInfo(message.ToFormat(parameters));
        }

        public void LogInfo(string message)
        {
            writeLogToConsole("INFO", message);
        }

        public void LogWarn(string message, params object[] parameters)
        {
            LogWarn(message.ToFormat(parameters));
        }

        public void LogWarn(string message)
        {
            writeLogToConsole("WARN", message);
        }

        public void LogError(string message, params object[] parameters)
        {
            LogError(message.ToFormat(parameters));
        }

        public void LogError(string message)
        {
            writeLogToConsole("ERROR", message);
        }

        public void LogFatal(string message, params object[] parameters)
        {
            LogFatal(message.ToFormat(parameters));
        }

        public void LogFatal(string message)
        {
            writeLogToConsole("FATAL", message);
        }

        public IDisposable Push(string context)
        {
            _context = context;
            return this;
        }

        public void LogException(Exception exception)
        {
            LogError(exception.ToString());
        }

        public void Dispose()
        {
            _context = null;
        }
    }


    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log;

        public Log4NetLogger(Type type)
        {
            _log = LogManager.GetLogger(type.Assembly, type);
        }

        public void LogDebug(string message, params object[] parameters)
        {
            _log.DebugFormat(message, parameters);
        }

        public void LogDebug(string message)
        {
            _log.Debug(message);
        }

        public void LogInfo(string message, params object[] parameters)
        {
            _log.InfoFormat(message, parameters);
        }

        public void LogInfo(string message)
        {
            _log.Info(message);
        }

        public void LogWarn(string message, params object[] parameters)
        {
            _log.WarnFormat(message, parameters);
        }

        public void LogWarn(string message)
        {
            _log.Warn(message);
        }

        public void LogError(string message, params object[] parameters)
        {
            _log.ErrorFormat(message, parameters);
        }

        public void LogError(string message)
        {
            _log.Error(message);
        }

        public void LogFatal(string message, params object[] parameters)
        {
            _log.FatalFormat(message, parameters);
        }

        public void LogFatal(string message)
        {
            _log.Fatal(message);
        }

        public void LogException(Exception exception)
        {
            _log.Error(exception);
        }

        public IDisposable Push(string context)
        {
            return NDC.Push(context);
        }
    }
}
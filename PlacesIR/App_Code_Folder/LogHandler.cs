using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PlacesIR
{
    public static class LogHandler
    {
        private static readonly string LOG_FILE_TEXT;
        private static NLog.Logger logger;

        static LogHandler()
        {
            try
            {
                string directory;
                if (HttpContext.Current != null)
                {
                    directory = HttpContext.Current.Server.MapPath("~/App_Data/Logs/");
                }
                else
                {
                    directory = HttpRuntime.AppDomainAppVirtualPath + "\\App_Data\\Logs\\";
                }
                LOG_FILE_TEXT = directory + "log.txt";

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                WriteLog("LogHandler init: on directory " + directory);
                PlacesIR.Extentions.NlogInit.RegisterLayoutRenderer("fullurl", typeof(PlacesIR.Extentions.FullUrlLayoutRenderer));
                WriteLog("fullurl registered");
                logger = NLog.LogManager.GetCurrentClassLogger();
            }
            catch (Exception ex)
            {
                WriteLog("LogHandler init: " + ex.ToString() + (ex.InnerException != null ? " Inner exception = " + ex.InnerException.ToString() : ""));
            }
        }

        /// <summary>
        /// Using the NLog dll to write log
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="exception"></param>
        /// <param name="level"></param>
        public static void WriteLog(string key, string value, Exception exception = null, Level level = Level.Info)
        {
            try
            {
                string msg = key + "--" + value;
                if (logger == null)
                {
                    logger = NLog.LogManager.GetCurrentClassLogger();
                }
                switch (level)
                {
                    case Level.Trace:
                        logger.TraceException(msg, exception);
                        break;
                    case Level.Debug:
                        logger.DebugException(msg, exception);
                        break;
                    case Level.Info:
                        logger.InfoException(msg, exception);
                        break;
                    case Level.Warn:
                        logger.WarnException(msg, exception);
                        break;
                    case Level.Error:
                        logger.ErrorException(msg, exception);
                        break;
                    case Level.Fatal:
                        logger.FatalException(msg, exception);
                        break;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString() + (ex.InnerException != null ? " Inner exception = " + ex.InnerException.ToString() : ""));
            }
        }

        public static void WriteLog(string message)
        {
            try
            {
                StreamWriter sw = File.AppendText(LOG_FILE_TEXT);
                sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff") + " ==> " + message);
                sw.Close();
            }
            catch { }
        }

        public static void WriteLog(Exception ex)
        {
            WriteLog(ex.ToString());
        }
    }
    public enum Level
    {
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }
}
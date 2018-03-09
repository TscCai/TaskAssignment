using System;
using System.IO;
using System.Text;

namespace TaskAssignment.Util
{
    public class SimpleLogger
    {
        public enum LogLevel
        {
            // None mode log nothing.
            None,
            // Warning mode only log for exceptions.
            Warning,
            // Debug mode has the most detailed log.
            Debug
        }

        #region 日志输出
        private TextWriter logger;
        public string LogFile { get; set; }
        public LogLevel Level { get; set; }

        private SimpleLogger() {
            LogFile = AppDomain.CurrentDomain.BaseDirectory + "_Log.txt";
        }

        public SimpleLogger(string logFilename) {
            LogFile = logFilename;
        }

        private void InitLogger() {
            if (logger == null) {
                logger = new StreamWriter(new FileStream(LogFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite|FileShare.Delete));
            }
        }

        private void PrintTimestamp() {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            logger.Write(BoldBraceWraped(time));
            logger.Write(" ");
        }

        public void AppendLog(string format, params object[] arg) {
            if (Level != LogLevel.None) {
                InitLogger();
                PrintTimestamp();
                logger.WriteLine(format, arg);
                logger.Flush();
            }
        }
        public void AppendLog(object o) {
            if (Level != LogLevel.None) {
                InitLogger();
                PrintTimestamp();
                logger.WriteLine(o);
                logger.Flush();
            }
        }
        public void AppendLog(Exception ex) {
            if (Level != LogLevel.None) {
                InitLogger();
                PrintTimestamp();
                logger.WriteLine(ex.ToString());
            }
        }

        public void Close() {
            if (logger != null) {
                logger.Flush();
                logger.Close();
            }
        }

        private string BoldBraceWraped(object str) {
            StringBuilder sb = new StringBuilder();
            sb.Append("[").Append(str).Append("]");
            return sb.ToString();
        }

        #endregion
    }
}

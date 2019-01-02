using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

namespace Utilities
{
    /// <summary>
    /// A simple log class that allows to discreminately create different
    /// log entries of different severity types.
    /// </summary>
    public class Logger
    {
        string logfile;
        public String Logfile 
        {
            set
            {
                logfile = value;
            }
            get
            {
                return logfile;
            }
        }
        /// <summary>
        /// A constructor that allows to set a fully-qualified filename.
        /// </summary>
        /// <param name="file"></param>
        public Logger(String file)
        {
            logfile = file;
            Init();
        }

        private void Init()
        {
            // The following stream allows sharing of the log file with an external process
            Stream myFile = new FileStream(logfile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            /* Create a new text writer using the output stream, and add it to
             * the trace listeners. */
            TextWriterTraceListener myTextListener = new
               TextWriterTraceListener(myFile);
            Trace.Listeners.Add(myTextListener);
            Trace.AutoFlush = true;
        }
        /// <summary>
        /// Writes and error message entry
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="module"></param>
        public void Error(string message, [CallerMemberName]string module = "")
        {
            WriteEntry(message, "[ERROR]", module);
        }
        /// <summary>
        /// Writes an Error log entry for an exception
        /// </summary>
        /// <param name="ex">The exception object</param>
        /// <param name="module"></param>
        public void Error(Exception ex, [CallerMemberName]string module = "")
        {
            WriteEntry(ex.Message, "[ERROR]", module);
            WriteEntry(ex.ToString(), "[ERROR]", module);
        }
        /// <summary>
        /// Writes a warning message entry
        /// </summary>
        /// <param name="message"></param>
        /// <param name="module"></param>
        public void Warning(string message, [CallerMemberName]string module = "")
        {
            WriteEntry(message, "[WARNING]", module);
        }
        /// <summary>
        /// Writes an info message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="module"></param>
        public void Info(string message, [CallerMemberName]string module = "")
        {
            WriteEntry(message, "[INFO]", module);
        }
        /// <summary>
        /// A helper method to write a log entry.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="module"></param>
        private void WriteEntry(string message, string type, string module)
        {
            // Stream myFile = File.OpenWrite(logfile);

            Trace.WriteLine(
                    string.Format("{0},{1},{2},{3}",
                                  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                  type,
                                  module,
                                  message));
            Trace.Flush();
        }
    }
}
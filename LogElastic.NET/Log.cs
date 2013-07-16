using System;

namespace LogElastic.NET
{
    public static class Log
    {
        private const string TRACE_TYPE = "Trace";
        private const string INFO_TYPE = "Info";
        private const string ERROR_TYPE = "Error";
        public static event EventHandler<Entry> Entries;

        /// <summary>
        /// Logs the specified message as an Trace <see cref="Entry"/>
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        public static void Trace(string message, params object[] args)
        {
            if (Entries != null)
            {
                Entries(null, new Entry { Message = string.Format(message, args), Severity = TRACE_TYPE });
            }
        }

        /// <summary>
        /// Logs the specified message as an Info <see cref="Entry"/>
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        public static void Info(string message, params object[] args)
        {
            if (Entries != null)
            {
                Entries(null, new Entry { Message = string.Format(message, args), Severity = INFO_TYPE });
            }
        }

        /// <summary>
        /// Logs the specified message as an Error <see cref="Entry"/>
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        public static void Error(string message, params object[] args)
        {
            if (Entries != null)
            {
                Entries(null, new Entry { Message = string.Format(message, args), Severity = ERROR_TYPE });
            }
        }
    }
}

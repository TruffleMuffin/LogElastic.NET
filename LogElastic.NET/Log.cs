﻿using System;

namespace LogElastic.NET
{
    /// <summary>
    /// Provides access to logging functionality.
    /// </summary>
    public static class Log
    {
        private static ElasticSearchStorage storage;

        /// <summary>
        /// The string value representing Trace Log <see cref="Entry"/>.
        /// </summary>
        private const string TRACE_TYPE = "Trace";

        /// <summary>
        /// The string value representing Info Log <see cref="Entry"/>.
        /// </summary>
        private const string INFO_TYPE = "Info";

        /// <summary>
        /// The string value representing Error Log <see cref="Entry"/>.
        /// </summary>
        private const string ERROR_TYPE = "Error";

        /// <summary>
        /// The Event that is raised when a <see cref="Entry"/> is created
        /// </summary>
        public static event EventHandler<Entry> Entries;

        /// <summary>
        /// Gets an instance of a <see cref="ILog"/> logger.
        /// </summary>
        /// <returns>A logger</returns>
        public static ILog GetLogger()
        {
            return new DebugOnlyLogger();
        }

        /// <summary>
        /// Initialises default logging.
        /// </summary>
        public static void Initialise(string server = "localhost")
        {
            if (Settings.LoggingEnabled)
            {
                if (storage == null) storage = new ElasticSearchStorage(server);
            }
        }

        /// <summary>
        /// Disables logging.
        /// </summary>
        public static void Disable()
        {
            if (Settings.LoggingEnabled)
            {
                if (storage != null)
                {
                    storage.Dispose();
                    storage = null;
                }
            }
        }

        /// <summary>
        /// Logs the specified message as a Trace <see cref="Entry"/>
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        public static void Trace(string message, params object[] args)
        {
            if (Settings.LoggingEnabled && Entries != null)
            {
                Entries(null, new Entry { Message = string.Format(message, args), Severity = TRACE_TYPE });
            }
        }

        /// <summary>
        /// Logs the specified message as a Info <see cref="Entry"/>
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        public static void Info(string message, params object[] args)
        {
            if (Settings.LoggingEnabled && Entries != null)
            {
                Entries(null, new Entry { Message = string.Format(message, args), Severity = INFO_TYPE });
            }
        }

        /// <summary>
        /// Logs the specified message as a Error <see cref="Entry"/>
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        public static void Error(string message, params object[] args)
        {
            if (Settings.LoggingEnabled && Entries != null)
            {
                Entries(null, new Entry { Message = string.Format(message, args), Severity = ERROR_TYPE });
            }
        }
    }
}

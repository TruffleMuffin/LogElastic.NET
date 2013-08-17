namespace LogElastic.NET
{
    /// <summary>
    /// An implementation of <see cref="ILog"/> that uses the DEBUG compile constant to remove logging overhead.
    /// </summary>
    internal class DebugOnlyLogger : ILog
    {
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // No resources to dispose
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ILog" /> is enabled. This indicates the current value of the global setting for
        /// turning logging output on and off.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled
        {
            get
            {
#if DEBUG
                return Settings.LoggingEnabled;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// Logs the specified <see cref="Entry"/>.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public void This(Entry entry)
        {
#if DEBUG
            Log.This(entry);
#endif
        }

        /// <summary>
        /// Logs the specified message as a Trace <see cref="Entry" />
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        public void Trace(string message, params object[] args)
        {
#if DEBUG
            Log.Trace(message, args);
#endif
        }

        /// <summary>
        /// Logs the specified message as a Info <see cref="Entry" />
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        public void Info(string message, params object[] args)
        {
#if DEBUG
            Log.Info(message, args);
#endif
        }

        /// <summary>
        /// Logs the specified message as a Error <see cref="Entry" />
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        public void Error(string message, params object[] args)
        {
#if DEBUG
            Log.Error(message, args);
#endif
        }
    }
}
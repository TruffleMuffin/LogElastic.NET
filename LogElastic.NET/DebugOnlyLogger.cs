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
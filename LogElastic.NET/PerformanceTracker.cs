using System;
using System.Diagnostics;

namespace LogElastic.NET
{
    /// <summary>
    /// Describes a <see cref="IDisposable"/> object that can track performance using <see cref="Log"/>.
    /// </summary>
    public class PerformanceTracker : IDisposable
    {
        private readonly ILog logger;
        private readonly DateTime start;
        private readonly string name;
        private const string LOG_FORMAT = "'{0}' executed in '{1}'";

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceTracker" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="name">The name.</param>
        public PerformanceTracker(ILog logger, string name = null)
        {
            this.logger = logger;
            this.name = name;

            // Following actions likely incur some overhead, only action them if logging is actually enabled.
            if (this.logger.Enabled)
            {
                // If there is no name set, pull the calling method and type and use that instead
                if (string.IsNullOrWhiteSpace(this.name))
                {
                    var frame = new StackFrame(2, false);
                    this.name = frame.GetMethod().DeclaringType + frame.GetMethod().Name;
                }

                this.start = DateTime.Now;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            logger.Trace(LOG_FORMAT, name, DateTime.Now - start);
            logger.Dispose();
        }
    }
}
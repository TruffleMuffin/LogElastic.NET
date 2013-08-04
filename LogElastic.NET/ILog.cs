using System;

namespace LogElastic.NET
{
    /// <summary>
    /// Describes a Logger
    /// </summary>
    public interface ILog : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="ILog"/> is enabled. This indicates the current value of the global setting for
        /// turning logging output on and off.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        bool Enabled { get; }

        /// <summary>
        /// Logs the specified message as a Trace <see cref="Entry"/>
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        void Trace(string message, params object[] args);

        /// <summary>
        /// Logs the specified message as a Info <see cref="Entry"/>
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        void Info(string message, params object[] args);

        /// <summary>
        /// Logs the specified message as a Error <see cref="Entry"/>
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        void Error(string message, params object[] args);
    }
}
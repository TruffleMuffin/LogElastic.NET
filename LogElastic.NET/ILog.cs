using System;

namespace LogElastic.NET
{
    /// <summary>
    /// Describes a Logger
    /// </summary>
    public interface ILog : IDisposable
    {
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
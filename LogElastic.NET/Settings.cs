using System.Configuration;

namespace LogElastic.NET
{
    /// <summary>
    /// Settings for LogElastic.NET
    /// </summary>
    internal static class Settings
    {
        /// <summary>
        /// Initializes the <see cref="Settings"/> class.
        /// </summary>
        static Settings()
        {
            var setting = ConfigurationManager.AppSettings["LogElastic.Enabled"];

            bool enabled;
            if (bool.TryParse(setting, out enabled))
            {
                LoggingEnabled = enabled;
            }
        }

        /// <summary>
        /// Gets a value indicating whether logging should be enabled for this application.
        /// </summary>
        internal static bool LoggingEnabled { get; set; }
    }
}

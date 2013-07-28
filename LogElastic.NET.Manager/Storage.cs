
namespace LogElastic.NET.Manager
{
    /// <summary>
    /// Storage control for LogElastic.NET
    /// </summary>
    public static class Storage
    {
        private static ElasticSearchStorage storage;

        /// <summary>
        /// Initialises LogElastic.NET Storage.
        /// </summary>
        public static void Initialise()
        {
            if (storage == null) storage = new ElasticSearchStorage();
        }

        /// <summary>
        /// Disables LogElastic.NET Storage.
        /// </summary>
        public static void Disable()
        {
            if (storage != null) storage.Dispose();
        }
    }
}

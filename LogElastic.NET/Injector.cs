namespace LogElastic.NET
{
    /// <summary>
    /// The Injector (dependency injection) static class.
    /// </summary>
    public class Injector
    {
        /// <summary>
        /// The resolver to use for IoC
        /// </summary>
        private static IResolver resolver;

        /// <summary>
        /// Gets or sets the resolver.
        /// </summary>
        /// <value>
        /// The resolver.
        /// </value>
        public static IResolver Resolver
        {
            get { return resolver ?? (resolver = new InternalResolver()); }
            set { resolver = value; }
        }

        /// <summary>
        /// Gets an instance.
        /// </summary>
        /// <typeparam name="T">The type of the instance.</typeparam>
        /// <returns>The instance.</returns>
        public static T Get<T>()
        {
            return Resolver.Get<T>();
        }
    }
}
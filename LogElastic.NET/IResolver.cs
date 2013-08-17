namespace LogElastic.NET
{
    /// <summary>
    /// A resolver supporting the IoC (dependency injection) class.
    /// </summary>
    public interface IResolver
    {
        /// <summary>
        /// Gets an instance.
        /// </summary>
        /// <typeparam name="T">The type of the service to get</typeparam>
        /// <returns>
        /// The service instance
        /// </returns>
        T Get<T>();    
    }
}
using System;
using System.Collections.Generic;

namespace LogElastic.NET
{
    /// <summary>
    /// Simple resolver for <see cref="Injector"/>
    /// </summary>
    internal class InternalResolver : IResolver
    {
        public static readonly IDictionary<Type, object> ResolveMap = new Dictionary<Type, object>
            {
                { typeof(ILog), new DebugOnlyLogger() }
            };

        /// <summary>
        /// Gets an instance.
        /// </summary>
        /// <typeparam name="T">The type of the service to get</typeparam>
        /// <returns>
        /// The service instance
        /// </returns>
        public T Get<T>()
        {
            return (T)ResolveMap[typeof(T)];
        }
    }
}
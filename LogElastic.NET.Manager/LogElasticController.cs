using System.Web.Http;

namespace LogElastic.NET.Manager
{
    /// <summary>
    /// Endpoint for controlling LogElastic.NET
    /// </summary>
    public class LogElasticController : ApiController
    {
        /// <summary>
        /// Initializes the <see cref="LogElasticController"/> class.
        /// </summary>
        static LogElasticController()
        {
            if (InternalResolver.ResolveMap.ContainsKey(typeof (ElasticSearchStorage)) == false)
            {
                InternalResolver.ResolveMap.Add(typeof(ElasticSearchStorage), new ElasticSearchStorage());
            }    
        }

        /// <summary>
        /// Action to turn the LogElastic.NET on and off
        /// </summary>
        /// <param name="enabled">if set to <c>true</c> initialises and turns on LogElastic.NET logging.</param>
        public void Get(bool enabled)
        {
            if (enabled)
            {
                Settings.LoggingEnabled = true;
                Storage.Initialise();
            }
            else
            {
                Settings.LoggingEnabled = false;
                Storage.Disable();
            }
        }
    }
}

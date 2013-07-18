using System;
using System.Web;
using Newtonsoft.Json;

namespace LogElastic.NET
{
    /// <summary>
    /// Extra information stored inside <see cref="Entry"/> as part of the LogStash standard.
    /// </summary>
    [Serializable]
    public sealed class EntryFields
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntryFields"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        public EntryFields(HttpRequestBase request)
        {
            this.Request = request.HttpMethod + " " + request.Url.PathAndQuery;
            this.UserAgent = request.UserAgent;
        }

        /// <summary>
        /// Gets or sets the details of the Http Request.
        /// </summary>
        [JsonProperty("request")]
        public string Request { get; set; }

        /// <summary>
        /// Gets or sets the user agent.
        /// </summary>
        [JsonProperty("user-agent")]
        public string UserAgent { get; set; }
    }
}
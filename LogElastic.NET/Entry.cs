using Newtonsoft.Json;
using System;
using System.Web;

namespace LogElastic.NET
{
    /// <summary>
    /// The Event Arguments for a Log Entry
    /// </summary>
    [Serializable]
    public sealed class Entry : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entry"/> class.
        /// </summary>
        public Entry()
            : this(HttpContext.Current != null ? new HttpRequestWrapper(HttpContext.Current.Request) : null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entry" /> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="timestamp">Optioanlly, the timestamp, will be set automatically if not provided.</param>
        public Entry(HttpRequestBase request, DateTime? timestamp = null)
        {
            if (request != null)
            {
                Fields = new EntryFields(request);
                SourceHost = request.UserHostAddress;
                SourcePath = request.HttpMethod + " " + request.Url.PathAndQuery;
            }
            Id = Guid.NewGuid();
            Timestamp = timestamp ?? DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the log message.
        /// </summary>
        [JsonProperty("@message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the severity of the <see cref="Entry"/>.
        /// </summary>
        [JsonProperty("@type")]
        public string Severity { get; set; }

        /// <summary>
        /// Gets the date and time that the <see cref="Entry"/> was raised.
        /// </summary>
        [JsonProperty("@timestamp")]
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Gets the source of this <see cref="Entry"/>.
        /// </summary>
        [JsonProperty("@source")]
        public string Source { get { return "mls"; } }

        /// <summary>
        /// Gets the tags of this <see cref="Entry"/>.
        /// </summary>
        [JsonProperty("@tags")]
        public string[] Tags { get { return new string[0]; } }

        /// <summary>
        /// Gets the source host of the user that caused this <see cref="Entry"/>.
        /// </summary>
        [JsonProperty("@source_host")]
        public string SourceHost { get; private set; }

        /// <summary>
        /// Gets the http method and path/query string.
        /// </summary>
        [JsonProperty("@source_path")]
        public string SourcePath { get; private set; }

        /// <summary>
        /// Gets the fields representing extra meta data about this <see cref="Entry"/>.
        /// </summary>
        [JsonProperty("@fields")]
        public EntryFields Fields { get; private set; }

        /// <summary>
        /// Gets the id of the <see cref="Entry"/>.
        /// </summary>
        internal Guid Id { get; private set; }
    }
}
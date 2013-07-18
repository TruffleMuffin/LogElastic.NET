using Newtonsoft.Json;
using System;
using System.Web;

namespace LogElastic.NET
{
    /// <summary>
    /// The Event Arguments for a Log Entry
    /// </summary>
    public class Entry : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entry"/> class.
        /// </summary>
        public Entry()
        {
            Request = HttpContext.Current != null ? HttpContext.Current.Request : null;
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
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
        public string SourceHost { get { return Request == null ? string.Empty : Request.UserHostAddress; } }

        /// <summary>
        /// Gets the http method and path/query string.
        /// </summary>
        [JsonProperty("@source_path")]
        public string SourcePath { get { return Request == null ? string.Empty : Request.HttpMethod + " " + Request.Url.PathAndQuery; } }

        /// <summary>
        /// Gets the fields representing extra meta data about this <see cref="Entry"/>.
        /// </summary>
        [JsonProperty("@fields")]
        public EntryFields Fields { get { return new EntryFields(Request); } }

        /// <summary>
        /// Gets the id of the <see cref="Entry"/>.
        /// </summary>
        internal Guid Id { get; private set; }

        /// <summary>
        /// Gets the request associated with the <see cref="Entry"/>.
        /// </summary>
        /// <remarks>Can be null</remarks>
        private HttpRequest Request { get; set; }
    }
}
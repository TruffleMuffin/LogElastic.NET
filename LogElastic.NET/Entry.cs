using System;
using System.Text;
using System.Web;
using Newtonsoft.Json;

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
            Response = HttpContext.Current != null ? HttpContext.Current.Response : null;

            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the log message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the severity of the <see cref="Entry"/>.
        /// </summary>
        public string Severity { get; set; }

        /// <summary>
        /// Gets the id of the <see cref="Entry"/>.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the date and time that the <see cref="Entry"/> was raised.
        /// </summary>
        [JsonProperty("@timestamp")]
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Gets the request associated with the <see cref="Entry"/>.
        /// </summary>
        /// <remarks>Can be null</remarks>
        private HttpRequest Request { get; set; }

        /// <summary>
        /// Gets the response.
        /// </summary>
        private HttpResponse Response { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            // Generate a LogStash return value
            var sb = new StringBuilder(100);
            sb.Append("{");
            sb.Append("\"@source\":\"mls\",");
            sb.Append("\"@tags\":[],");
            if (Request != null && Response != null)
            {
                sb.Append("\"@fields\": {");
                sb.AppendFormat("\"request\":\"{0} {1}\",", Request.HttpMethod, Request.Url.PathAndQuery);
                sb.AppendFormat("\"user-agent\":\"{0}\"", Request.UserAgent);
                sb.Append("},");
            }
            sb.AppendFormat("\"@timestamp\":\"{0}\",", Timestamp.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK"));
            sb.AppendFormat("\"@source_host\":\"{0}\",", Request == null ? string.Empty : Request.UserHostAddress);
            sb.AppendFormat("\"@source_path\":\"{0}\",", Request == null ? string.Empty : Request.HttpMethod + " " + Request.Url.PathAndQuery);
            sb.AppendFormat("\"@message\":\"{0}\",", Message);
            sb.AppendFormat("\"@type\":\"{0}\"", Severity);
            sb.Append("}");

            return sb.ToString();
        }
    }
}
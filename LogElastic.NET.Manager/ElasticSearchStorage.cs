using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using PlainElastic.Net;
using PlainElastic.Net.Serialization;
using PlainElastic.Net.Utils;

namespace LogElastic.NET
{
    /// <summary>
    /// Elastic Search storage for Logging.
    /// </summary>
    internal class ElasticSearchStorage : IDisposable
    {
        private readonly IElasticConnection connection;
        private readonly IDisposable observer;
        private readonly JsonNetSerializer serializer = new JsonNetSerializer();

        /// <summary>
        /// The format of the Index
        /// </summary>
        private const string INDEX_FORMAT = "logstash-{0}";

        /// <summary>
        /// The format of the Date in the Index
        /// </summary>
        private const string INDEX_DATE_FORMAT = "yyyy.MM.dd";

        /// <summary>
        /// The type of document that ElasticSearch should cluster entries by
        /// </summary>
        private const string LOG_TYPE = "Log";

        /// <summary>
        /// Gets or sets the time in seconds between exporting log entries.
        /// </summary>
        internal static int LogDelay { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElasticSearchStorage"/> class.
        /// </summary>
        public ElasticSearchStorage()
            : this("localhost")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElasticSearchStorage"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="port">The port.</param>
        public ElasticSearchStorage(string server, int port = 9200)
        {
            // Initialise the LogDelay to be a sensible default if not already set
            if (LogDelay == 0) LogDelay = 60;

            connection = new ElasticConnection(server, port);

            // Subscribe to Log Publishers
            observer = Observable
                .FromEventPattern<Entry>(ev => Log.Entries += ev, ev => Log.Entries -= ev)
                .Select(a => a.EventArgs)
                .Buffer(TimeSpan.FromSeconds(LogDelay))
                .Subscribe(Export);
        }

        /// <summary>
        /// Gets the index to store entries for.
        /// </summary>
        /// <returns>A day based index for the current <see cref="DateTime"/></returns>
        internal static string GetIndex()
        {
            return string.Format(INDEX_FORMAT, DateTime.UtcNow.Date.ToString(INDEX_DATE_FORMAT));
        }

        /// <summary>
        /// Exports the specified entry.
        /// </summary>
        /// <param name="entries">The entry.</param>
        internal void Export(IList<Entry> entries)
        {
            if (entries.Any() == false) return;

            // setup variable to group all error messages together
            var errorBuilder = new StringBuilder();

            // Build a batch based update to avoid memory overhead
            var bulkCommand = new BulkCommand(GetIndex(), LOG_TYPE);
            var bulkJsons = new BulkBuilder(serializer)
                .PipelineCollection(entries, (builder, entity) => builder.Index(entity, id: entity.Id.ToString()))
                .JoinInBatches(batchSize: 1000);

            foreach (var bulk in bulkJsons)
            {
                string result = connection.Put(bulkCommand, bulk);

                var bulkResult = serializer.ToBulkResult(result);

                // Check for errors
                foreach (var operation in bulkResult.items.Where(a => a.Result.ok == false))
                {
                    errorBuilder.AppendFormat("Id: {0} Error: {1} {2}", operation.Result._id, operation.Result.error, Environment.NewLine);
                }
            }

            // Check for any errors that are reported by the ElasticSearch
            var error = errorBuilder.ToString();
            if (string.IsNullOrWhiteSpace(error) == false) throw new ApplicationException(error);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            observer.Dispose();
        }
    }
}
using System;
using System.Threading;
using MbUnit.Framework;
using PlainElastic.Net;
using PlainElastic.Net.Queries;
using PlainElastic.Net.Serialization;

namespace LogElastic.NET.Tests
{
    [TestFixture]
    public class LogTests
    {
        private ElasticSearchStorage storage;

        [SetUp]
        void SetUp()
        {
            ElasticSearchStorage.LogDelay = 2;
            storage = new ElasticSearchStorage();
        }

        [TearDown]
        void TearDown()
        {
            storage.Dispose();
        }

        /// <summary>
        /// <see cref="Log"/> Integration Test
        /// </summary>
        [Test]
        void Log_Events_Expected()
        {
            // Dev note - this builds the index, but need to run tests again to work
            var connection = new ElasticConnection("localhost", 9200);
            if (IsIndexExists(ElasticSearchStorage.GetIndex(), connection))
            {
                connection.Delete(new DeleteCommand(ElasticSearchStorage.GetIndex()));
            }

            for (var i = 1; i <= 100; i++)
            {
                Log.Trace("Entry Message : {0}", i);
            }

            Thread.Sleep(TimeSpan.FromSeconds(5));

            var serializer = new JsonNetSerializer();

            // Build the Search Query
            var query = new QueryBuilder<Entry>().Build();
            
            // Execute the search
            string result = connection.Post(Commands.Search(ElasticSearchStorage.GetIndex(), "Log"), query);
            var searchResult = serializer.ToSearchResult<Entry>(result);

            // Check all log entries in search index
            Assert.AreEqual(100, searchResult.hits.total);
        }

        private static bool IsIndexExists(string indexName, ElasticConnection connection)
        {
            try
            {
                connection.Head(new IndexExistsCommand(indexName));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

#undef DEBUG

using System;
using System.Threading;
using LogElastic.NET.Manager;
using MbUnit.Framework;
using PlainElastic.Net;
using PlainElastic.Net.Queries;
using PlainElastic.Net.Serialization;

namespace LogElastic.NET.Tests
{
    [TestFixture]
    public class LogTests
    {
        [SetUp]
        void SetUp()
        {
            ElasticSearchStorage.LogDelay = 2;
            
            if (InternalResolver.ResolveMap.ContainsKey(typeof(ElasticSearchStorage)) == false)
            {
                InternalResolver.ResolveMap.Add(typeof(ElasticSearchStorage), new ElasticSearchStorage());
            }    

            Settings.LoggingEnabled = true;
            Storage.Initialise();
        }

        [TearDown]
        void TearDown()
        {
            Settings.LoggingEnabled = false;
            Storage.Disable();

            // Turn off listening to the Event
            Log.Entries -= Log_Entries;
        }

        /// <summary>
        /// <see cref="Log"/> Integration Test
        /// </summary>
        [Test(Order = 1)]
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
                Log.Info("Entry Message : {0}", i);
                Log.Error("Entry Message : {0}", i);
            }

            Thread.Sleep(TimeSpan.FromSeconds(5));

            var serializer = new JsonNetSerializer();

            // Build the Search Query
            var query = new QueryBuilder<Entry>().Build();

            // Execute the search
            string result = connection.Post(Commands.Search(ElasticSearchStorage.GetIndex(), "Log"), query);
            var searchResult = serializer.ToSearchResult<Entry>(result);

            // Check all log entries in search index
            Assert.AreEqual(300, searchResult.hits.total);

            // Secondary check, use the instance based loger
            using (var log = Log.GetLogger())
            {
                for (var i = 1; i <= 100; i++)
                {
                    log.Trace("Entry Message : {0}", i);
                    log.Info("Entry Message : {0}", i);
                    log.Error("Entry Message : {0}", i);
                }

                Thread.Sleep(TimeSpan.FromSeconds(5));

                // Build the Search Query
                query = new QueryBuilder<Entry>().Build();

                // Execute the search
                result = connection.Post(Commands.Search(ElasticSearchStorage.GetIndex(), "Log"), query);
                searchResult = serializer.ToSearchResult<Entry>(result);

                // Check all log entries in search index
                Assert.AreEqual(600, searchResult.hits.total);
            }
        }

        /// <summary>
        /// <see cref="Log"/> Integration Test
        /// </summary>
        [Test(Order = 2)]
        void Log_Disabled_Events_Expected()
        {
            Settings.LoggingEnabled = false;

            // Dev note - this builds the index, but need to run tests again to work
            var connection = new ElasticConnection("localhost", 9200);

            for (var i = 1; i <= 100; i++)
            {
                Log.Trace("Entry Message : {0}", i);
                Log.Info("Entry Message : {0}", i);
                Log.Error("Entry Message : {0}", i);
            }

            Thread.Sleep(TimeSpan.FromSeconds(5));

            var serializer = new JsonNetSerializer();

            // Build the Search Query
            var query = new QueryBuilder<Entry>().Build();

            // Execute the search
            string result = connection.Post(Commands.Search(ElasticSearchStorage.GetIndex(), "Log"), query);
            var searchResult = serializer.ToSearchResult<Entry>(result);

            // Check all log entries in search index
            Assert.AreEqual(600, searchResult.hits.total);
        }

        [Test(Order = 3)]
        void Log_Performance_Expected()
        {
            // Disable logging to elastic search, we will test with manually events
            Storage.Disable();

            // Listen to the Event
            Log.Entries += Log_Entries;

            using (Log.Performance())
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
        }

        void Log_Entries(object sender, Entry e)
        {
            Assert.AreEqual(null, sender);
            Assert.StartsWith(e.Message, "\'LogElastic.NET.Tests.LogTestsLog_Performance_Expected\' executed in ");
            Assert.Contains(e.Message, "\'00:00:02");
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

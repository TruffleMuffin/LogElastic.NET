using System;
using System.Threading;
using MbUnit.Framework;

namespace LogElastic.NET.Tests
{
    [TestFixture]
    public class LogTests
    {
        private ElasticSearchStorage storage;

        [SetUp]
        void SetUp()
        {
            storage = new ElasticSearchStorage();
        }

        [TearDown]
        void TearDown()
        {
            storage.Dispose();
        }

        [Test]
        void Log_Events_Expected()
        {
            for (var i = 1; i < 100; i++)
            {
                Log.Trace("Shit is happening : {0}", i);
            }

            Thread.Sleep(TimeSpan.FromMinutes(1));
        }
    }
}

using System;
using System.Web;
using MbUnit.Framework;
using PlainElastic.Net.Serialization;
using Rhino.Mocks;

namespace LogElastic.NET.Tests
{
    [TestFixture]
    class EntryTests
    {
        private MockRepository mocks;
        private Entry target;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
        }

        [Test]
        public void Serializable_True()
        {
            target = new Entry();

            Assert.BinarySerializeThenDeserialize(target);
        }

        [Test]
        public void JsonSerialization_ExpectedLogStashFormat()
        {
            var request = mocks.StrictMock<HttpRequestBase>();
            var timestamp = DateTime.Parse("2013-07-18T11:13:32.3149976+01:00");

            using (mocks.Record())
            {
                SetupResult.For(request.UserHostAddress).Return("http://192.168.0.1");
                SetupResult.For(request.HttpMethod).Return("GET");
                SetupResult.For(request.UserAgent).Return("Chrome");
                SetupResult.For(request.Url).Return(new Uri("http://192.168.0.2/Some/Path/?query=somewhere"));
            }
            using (mocks.Playback())
            {
                target = new Entry(request, timestamp)
                    {
                        Message = "My Message",
                        Severity = "Trace"
                    };
                var serializer = new JsonNetSerializer();
                var result = serializer.Serialize(target);
                var expected = "{\"@message\":\"My Message\",\"@type\":\"Trace\",\"@timestamp\":\"2013-07-18T11:13:32.3149976+01:00\",\"@source_host\":\"http://192.168.0.1\",\"@source_path\":\"GET /Some/Path/?query=somewhere\",\"@fields\":{\"request\":\"GET /Some/Path/?query=somewhere\",\"user-agent\":\"Chrome\"}}";
                Assert.AreEqual(expected, result);
            }
        }
    }
}

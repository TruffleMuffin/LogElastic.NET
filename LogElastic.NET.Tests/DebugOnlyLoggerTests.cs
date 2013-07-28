#define DEBUG

using MbUnit.Framework;

namespace LogElastic.NET.Tests
{

    [TestFixture]
    public class DebugOnlyLoggerTests
    {
        private DebugOnlyLogger target;

        [SetUp]
        public void SetUp()
        {
            target = new DebugOnlyLogger();
            Log.Entries += LogFailure;
        }

        [TearDown]
        public void TearDown()
        {
            Log.Entries -= LogFailure;
        }

        [Test]
        public void Info_WhenInDebugMode_DoesntLog()
        {
            target.Info("My Message");
        }

        [Test]
        public void Trace_WhenInDebugMode_DoesntLog()
        {
            target.Trace("My Message");
        }

        [Test]
        public void Error_WhenInDebugMode_DoesntLog()
        {
            target.Error("My Message");
        }

        private void LogFailure(object sender, Entry args)
        {
            Assert.Fail("Something was logged");
        }
    }
}
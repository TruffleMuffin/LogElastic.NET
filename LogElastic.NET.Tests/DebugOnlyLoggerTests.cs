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
        }

        [Test]
        public void Info_WhenInDebugMode_DoesntLog()
        {
            Log.Entries += (sender, args) => Assert.Fail("Something was logged");
            target.Info("My Message");
        }

        [Test]
        public void Trace_WhenInDebugMode_DoesntLog()
        {
            Log.Entries += (sender, args) => Assert.Fail("Something was logged");
            target.Trace("My Message");
        }

        [Test]
        public void Error_WhenInDebugMode_DoesntLog()
        {
            Log.Entries += (sender, args) => Assert.Fail("Something was logged");
            target.Error("My Message");
        }
    }
}
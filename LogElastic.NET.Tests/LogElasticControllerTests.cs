using LogElastic.NET.Manager;
using MbUnit.Framework;

namespace LogElastic.NET.Tests
{
    [TestFixture]
    public class LogElasticControllerTests
    {
        [Test]
        public void Static_Constructor_AddToResolver()
        {
            var controller = new LogElasticController();

            Assert.IsTrue(InternalResolver.ResolveMap.ContainsKey(typeof(ElasticSearchStorage)));
        }
    }
}
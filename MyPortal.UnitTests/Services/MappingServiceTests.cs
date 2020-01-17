using MyPortal.BusinessLogic.Services;
using NUnit.Framework;

namespace MyPortal.UnitTests.Services
{
    [TestFixture]
    public class MappingServiceTests
    {
        [Test]
        public void MappingBusinessConfiguration_IsValid()
        {
            Assert.DoesNotThrow(() => MappingService.GetMapperBusinessConfiguration().ConfigurationProvider.AssertConfigurationIsValid());
        }

        [Test]
        public void MappingDataGridConfiguration_IsValid()
        {
            Assert.DoesNotThrow(() => MappingService.GetMapperDataGridConfiguration().ConfigurationProvider.AssertConfigurationIsValid());
        }
    }
}

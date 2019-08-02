using AutoMapper;
using MyPortal.Models.Database;
using MyPortal.UnitTests.TestData;
using NUnit.Framework;

namespace MyPortal.UnitTests.ProcessTests
{
    [TestFixture]
    public class MyPortalTestFixture
    {
        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
        }

        [SetUp]
        public void Setup()
        {
            EffortProviderFactory.ResetDb();
            
            Mapper.Reset();
            ContextControl.InitialiseMaps();
        }
        
        [OneTimeTearDown]
        public void Clear()
        {
        }
    }
}
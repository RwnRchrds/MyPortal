using AutoMapper;
using MyPortal.Models.Database;
using MyPortal.UnitTests.TestData;
using NUnit.Framework;

namespace MyPortal.UnitTests.ProcessTests
{
    [TestFixture]
    public class MyPortalTestFixture
    {
        protected static MyPortalDbContext _context;
        
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
            _context = ContextControl.GetTestData();
            ContextControl.InitialiseMaps();
        }
        
        [OneTimeTearDown]
        public void Clear()
        {
            _context.Dispose();
        }
    }
}
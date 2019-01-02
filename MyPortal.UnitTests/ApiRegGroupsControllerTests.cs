using System.Linq;
using AutoMapper;
using MyPortal.Controllers.Api;
using MyPortal.Models;
using Effort;
using NUnit.Framework;

namespace MyPortal.UnitTests
{
    [TestFixture]
    public class ApiRegGroupsControllerTests
    {
        private RegGroupsController _controller;
        private MyPortalDbContext _context;

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

            _controller = new RegGroupsController(_context);
        }
        
        [OneTimeTearDown]
        public void Clear()
        {
            _controller.Dispose();
            _context.Dispose();
        }

        [Test]
        public void GetRegGroups_ReturnsRegGroups()
        {
            var yearGroup = _context.YearGroups.SingleOrDefault(x => x.Name == "Year 7");
            
            Assert.IsNotNull(yearGroup);
            
            var result = _controller.GetRegGroups(yearGroup.Id).Count();
            
            Assert.AreEqual(1, result);
        }
    }
}
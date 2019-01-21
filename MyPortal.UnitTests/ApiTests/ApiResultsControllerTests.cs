using System.Linq;
using AutoMapper;
using MyPortal.Controllers.Api;
using MyPortal.Models;
using MyPortal.UnitTests.TestData;
using NUnit.Framework;

namespace MyPortal.UnitTests.ApiTests
{
    [TestFixture]
    public class ApiResultsControllerTests
    {
        private ResultsController _controller;
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

            _controller = new ResultsController(_context);
        }
        
        [OneTimeTearDown]
        public void Clear()
        {
            _controller.Dispose();
            _context.Dispose();
        }

        [Test]
        public void GetResults_ReturnsResultsForStudent()
        {
            var student = _context.Students.SingleOrDefault(x => x.FirstName == "Aaron");

            var resultSet = _context.ResultSets.SingleOrDefault(x => x.Name == "Current");
            
            Assert.IsNotNull(student);
            Assert.IsNotNull(resultSet);

            var result = _controller.GetResults(student.Id, resultSet.Id);
            
            Assert.AreEqual(2, result.Count());
        }
    }
}
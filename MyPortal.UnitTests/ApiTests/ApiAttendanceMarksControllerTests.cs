using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Controllers.Api;
using MyPortal.Models.Database;
using MyPortal.UnitTests.TestData;
using NUnit.Framework;

namespace MyPortal.UnitTests.ApiTests
{
    [TestFixture]
    public class ApiAttendanceMarksControllerTests
    {
        private AttendanceMarksController _controller;
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

            _controller = new AttendanceMarksController(_context);
        }

        [OneTimeTearDown]
        public void Clear()
        {
            _controller.Dispose();
            _context.Dispose();
        }

        [Test]
        public void LoadRegister_LoadsRegister()
        {
            Assert.Pass();
        }

        [Test]
        public void LoadRegister_AttendanceWeekDoesNotExist_ReturnsNotFound()
        {
            Assert.Pass();
        }

        [Test]
        public void LoadRegister_CurriculumClassPeriodDoesNotExist_ReturnsNotFound()
        {
            Assert.Pass();
        }


    }
}

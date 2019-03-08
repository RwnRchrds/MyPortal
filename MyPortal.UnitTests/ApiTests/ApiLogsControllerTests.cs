using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using MyPortal.Controllers.Api;
using MyPortal.Models;
using MyPortal.UnitTests.TestData;
using NUnit.Framework;

namespace MyPortal.UnitTests.ApiTests
{
    [TestFixture]
    public class ApiLogsControllerTests
    {
        private LogsController _controller;
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

            _controller = new LogsController(_context);
        }
        
        [OneTimeTearDown]
        public void Clear()
        {
            _controller.Dispose();
            _context.Dispose();
        }

        [Test]
        public void GetLogs_GetsLogsForStudent()
        {
            var student = _context.Students.SingleOrDefault(x => x.FirstName == "John");
            
            Assert.IsNotNull(student);

            var result = _controller.GetLogs(student.Id);           
            
            Assert.AreEqual(4, result.Count());
            Assert.AreEqual(student.Id, result.First().StudentId);
        }

        [Test]
        public void GetLog_ReturnsLog()
        {
            var log = _context.Logs.SingleOrDefault(x => x.Message == "Test3");
            
            Assert.IsNotNull(log);

            var result = _controller.GetLog(log.Id);
            
            Assert.AreEqual(3, result.TypeId);
        }

        [Test]
        public void GetLog_LogDoesNotExist_ReturnsNotFound()
        {
            const int logId = 9999;

            var ex = Assert.Throws<HttpResponseException>(() => _controller.GetLog(logId));
            Assert.AreEqual(HttpStatusCode.NotFound,ex.Response.StatusCode);
        }

        [Test]
        public void CreateLog_CreatesNewLog()
        {
            var init = _context.Logs.Count();
            
            var student = _context.Students.SingleOrDefault(x => x.FirstName == "Aaron");
            var initForStudent = _context.Logs.Count(x => x.StudentId == student.Id);
            
            Assert.IsNotNull(student);

            var newLog = new Log
                {Date = DateTime.Now, Message = "CreateLog", TypeId = 1, AuthorId = 1, StudentId = student.Id};

            _controller.CreateLog((newLog));

            var result = _context.Logs.Count();

            var result2 = _context.Logs.Count(x => x.StudentId == student.Id);
            
            Assert.AreEqual(init + 1, result);
            Assert.AreEqual(initForStudent + 1, result2);
        }

        [Test]
        public void CreateLog_StaffMemberDoesNotExist_ReturnsNotFound()
        {
            var student = _context.Students.SingleOrDefault(x => x.FirstName == "Dorothy");

            var logType = _context.LogTypes.SingleOrDefault(x => x.Name == "Type 3");
            
            Assert.IsNotNull(logType);
            Assert.IsNotNull(student);

            var log = new Log
            {
                Date = DateTime.Today,
                AuthorId = 9999,
                StudentId = student.Id,
                Message = "Test",
                TypeId = logType.Id,
            };

            var actionResult = _controller.CreateLog((log));

            var result = actionResult as NegotiatedContentResult<string>;
            
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual("Staff member not found", result.Content);
        }

//        [Test]
//        public void CreateLog_InvalidData_ReturnsBadRequest()
//        {
//            var logType = _context.LogTypes.SingleOrDefault(x => x.Name == "Type 2");
//            
//            Assert.IsNotNull(logType);
//            
//            var log = new Log
//            {
//                Date = DateTime.Today,
//                AuthorId = 3,
//                StudentId = 1,
//                TypeId = logType.Id,
//            };
//
//            var actionResult = _controller.CreateLog(Mapper.Map<Log, LogDto>(log));
//
//            var result = actionResult as NegotiatedContentResult<string>;
//            
//            Assert.IsNotNull(result);
//            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//            Assert.AreEqual("Invalid data", result.Content);
//        }

        [Test]
        public void UpdateLog_UpdatesLog()
        {
            var log = _context.Logs.SingleOrDefault(x => x.Message == "Test3");
            
            Assert.IsNotNull(log);

            log.Message = "Hello World";

            _controller.UpdateLog((log));

            var result = _context.Logs.SingleOrDefault(x => x.Id == log.Id);
            
            Assert.IsNotNull(result);
            
            Assert.AreEqual("Hello World", result.Message);
        }

        [Test]
        public void DeleteLog_DeletesLog()
        {
            var init = _context.Logs.Count();

            var log = _context.Logs.SingleOrDefault(x => x.Message == "Test2");
            
            Assert.IsNotNull(log);

            _controller.DeleteLog(log.Id);

            var result = _context.Logs.Count();
            
            Assert.AreEqual(init - 1, result);
        }
    }
}
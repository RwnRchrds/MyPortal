using System;
using System.Data.Common;
using System.Linq;
using AutoMapper;
using Effort;
using MyPortal.Controllers.Api;
using MyPortal.Dtos;
using MyPortal.Models;
using NUnit.Framework;

namespace MyPortal.UnitTests
{
    [TestFixture]
    public class ApiDocumentsControllerTests
    {
        private DocumentsController _controller;
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

            _controller = new DocumentsController(_context);
        }       

        [Test]
        public void GetDocuments_ReturnsAllGeneralDocuments()
        {
            var result = _controller.GetDocuments();
            
            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public void GetApprovedDocuments_ReturnsApprovedGeneralDocumentsOnly()
        {
            var result = _controller.GetApprovedDocuments();
            
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetDocument_ReturnsCorrectDocument()
        {
            var document = _context.Documents.SingleOrDefault(x => x.Description == "Doc1");

            var result = _controller.GetDocument(document.Id);
            
            Assert.AreEqual(document.Url, result.Url);
        }

        [Test]
        public void AddDocument_AddsNewDocument()
        {
            var init = _context.Documents.Count();

            var uploader = _context.Staff.SingleOrDefault(x => x.FirstName == "Lily");
            
            var document = new Document()
            {
                Url = "http://ftp.test.com/DocAdd", Description = "Add Document Test", Date = DateTime.Today,
                Approved = false, UploaderId = uploader.Id
            };

            _controller.AddDocument(Mapper.Map<Document, DocumentDto>(document));

            var result = _context.Documents.Count();

            var newDocument = _context.Documents.SingleOrDefault(x => x.Url == "http://ftp.test.com/DocAdd");
            
            Assert.AreEqual(init + 1, result);
            Assert.AreEqual(true, newDocument.IsGeneral);
            Assert.AreEqual(false, newDocument.Approved);
        }

        [Test]
        public void RemoveDocument_RemovesDocument()
        {
            var init = _context.Documents.Count();

            var document = _context.Documents.SingleOrDefault(x => x.Description == "Doc1");

            _controller.RemoveDocument(document.Id);

            var result = _context.Documents.Count();
            
            Assert.AreEqual(init - 1, result);
        }
        
        [OneTimeTearDown]
        public void Clear()
        {
            _controller.Dispose();
            _context.Dispose();
        }
    }
}
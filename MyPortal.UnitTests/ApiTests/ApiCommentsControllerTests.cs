using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using MyPortal.Controllers.Api;
using MyPortal.Models;
using MyPortal.UnitTests.TestData;
using AutoMapper;
using NUnit.Framework.Internal;

namespace MyPortal.UnitTests.ApiTests
{
    [TestFixture]
    public class ApiCommentsControllerTests
    {
        private CommentsController _controller;
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

            _controller = new CommentsController(_context);
        }

        [OneTimeTearDown]
        public void Clear()
        {
            _controller.Dispose();
            _context.Dispose();
        }

        [Test]
        public void GetComments_GetsAllComments()
        {
            var result = _controller.GetComments().Count();

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result);
        }

        [Test]
        public void GetCommentsByCommentBank_GetsComments()
        {
            var commentBank = _context.CommentBanks.SingleOrDefault(x => x.Name == "Middle");

            var result = _controller.GetCommentsByCommentBank(commentBank.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());            
        }

        [Test]
        public void GetCommentById_GetsComment()
        {
            var comment = _context.Comments.SingleOrDefault(x => x.Value == "Hello");

            Assert.IsNotNull(comment);

            var result = _controller.GetCommentById(comment.Id);

            Assert.AreEqual("Hello", result.Value);
            Assert.AreEqual("Opening", result.CommentBank.Name);
        }

        [Test]
        public void GetCommentById_CommentDoesNotExist_ThrowsHttpNotFoundException()
        {
            const int commentId = 9999;

            var ex = Assert.Throws<HttpResponseException>(() => _controller.GetCommentById(commentId));

            Assert.AreEqual(HttpStatusCode.NotFound, ex.Response.StatusCode);
        }

        [Test]
        public void CreateComment_CreatesComment()
        {
            var commentBank = _context.CommentBanks.SingleOrDefault(x => x.Name == "Opening");

            Assert.IsNotNull(commentBank);

            var comment = new Comment {CommentBankId = commentBank.Id, Value = "Greetings!"};

            var initialAmount = _context.Comments.Count();

            var result = _controller.CreateComment(comment);

            var finalAmount = _context.Comments.Count();

            Assert.IsInstanceOf<OkNegotiatedContentResult<string>>(result);
            Assert.AreEqual(initialAmount + 1, finalAmount);
        }

//        [Test]
//        public void CreateComment_InvalidData_ReturnsBadRequest()
//        {
//            var comment = new Comment {Value = "Test"};
//
//            var result = _controller.CreateComment(comment) as NegotiatedContentResult<string>;
//            
//            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//            Assert.AreEqual("Invalid data", result.Content);
//        }

        [Test]
        public void UpdateComment_UpdatesComment()
        {
            var comment = _context.Comments.SingleOrDefault(x => x.Value == "Hello");

            Assert.IsNotNull(comment);

            comment.Value = "Hello!";

            var result = _controller.UpdateComment(comment);

            Assert.IsInstanceOf<OkNegotiatedContentResult<string>>(result);
        }

        [Test]
        public void UpdateComment_CommentDoesNotExist_ReturnsNotFound()
        {
            var comment = new Comment { Id = 9999, Value = "Hello!"};

            var result = _controller.UpdateComment(comment) as NegotiatedContentResult<string>;

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual("Comment not found", result.Content);
        }

        [Test]
        public void DeleteComment_DeletesComment()
        {
            var comment = _context.Comments.SingleOrDefault(x => x.Value == "Hello");

            Assert.IsNotNull(comment);

            var initialAmount = _context.Comments.Count();

            var result = _controller.DeleteComment(comment.Id);

            var finalAmount = _context.Comments.Count();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkNegotiatedContentResult<string>>(result);
            Assert.AreEqual(initialAmount - 1, finalAmount);
        }

        [Test]
        public void DeleteComment_CommentDoesNotExist_ReturnsNotFound()
        {
            const int commentId = 9999;

            var result = _controller.DeleteComment(commentId) as NegotiatedContentResult<string>;

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual("Comment not found", result.Content);
        }
    }
}

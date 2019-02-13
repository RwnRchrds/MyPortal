using System.Linq;
using System.Net;
using System.Web.Http.Results;
using AutoMapper;
using MyPortal.Controllers.Api;
using MyPortal.Models;
using MyPortal.UnitTests.TestData;
using NUnit.Framework;

namespace MyPortal.UnitTests.ApiTests
{
    [TestFixture]
    public class ApiCommentBanksControllerTests
    {
        private CommentBanksController _controller;
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

            _controller = new CommentBanksController(_context);
        }
        
        [OneTimeTearDown]
        public void Clear()
        {
            _controller.Dispose();
            _context.Dispose();
        }

        [Test]
        public void GetCommentBanks_ReturnsCommentBanks()
        {
            var result = _controller.GetCommentBanks();
            
            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public void CreateCommentBank_CreatesCommentBank()
        {
            var commentBank = new CommentBank { Name = "New" };

            var initialAmount = _context.CommentBanks.Count();

            var result = _controller.CreateCommentBank(commentBank);

            var finalAmount = _context.CommentBanks.Count();
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkNegotiatedContentResult<string>>(result);
            Assert.AreEqual(initialAmount + 1, finalAmount);
        }

        [Test]
        public void CreateCommentBank_CommentBankAlreadyExists_ReturnsBadRequest()
        {
            var commentBank = new CommentBank {Name = "Opening"};

            var result = _controller.CreateCommentBank(commentBank) as NegotiatedContentResult<string>;
            
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual("Comment bank already exists", result.Content);
        }

        [Test]
        public void CreateCommentBank_InvalidData_ReturnsBadRequest()
        {
            var commentBank = new CommentBank();

            var result = _controller.CreateCommentBank(commentBank) as NegotiatedContentResult<string>;
            
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual("Invalid data", result.Content);
        }

        [Test]
        public void UpdateCommentBank_UpdatesCommentBank()
        {
            var commentBank = _context.CommentBanks.SingleOrDefault(x => x.Name == "Closing");
            
            Assert.IsNotNull(commentBank);
            
            commentBank.Name = "Ending";

            var result = _controller.UpdateCommentBank(commentBank);
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkNegotiatedContentResult<string>>(result);
        }

        [Test]
        public void UpdateCommentBank_CommentBankDoesNotExist_ReturnsNotFound()
        {
            var commentBank = new CommentBank{Id = 9999, Name="Ending"};

            var result = _controller.UpdateCommentBank(commentBank) as NegotiatedContentResult<string>;
            
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual("Comment bank not found", result.Content);
        }

        [Test]
        public void DeleteCommentBank_DeletesCommentBank()
        {
            var commentBank = _context.CommentBanks.SingleOrDefault(x => x.Name == "Middle");
            
            Assert.IsNotNull(commentBank);

            var initialAmount = _context.CommentBanks.Count();

            var result = _controller.DeleteCommentBank(commentBank.Id);

            var finalAmount = _context.CommentBanks.Count();
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkNegotiatedContentResult<string>>(result);
            Assert.AreEqual(initialAmount - 1, finalAmount);
        }

        [Test]
        public void DeleteCommentBank_CommentBankDoesNotExist_ReturnsNotFound()
        {
            const int commentBankId = 9999;

            var result = _controller.DeleteCommentBank(commentBankId) as NegotiatedContentResult<string>;
            
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual("Comment bank not found", result.Content);
        }
    }
}
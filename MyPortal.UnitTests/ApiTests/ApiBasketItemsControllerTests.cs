using System.Linq;
using System.Net;
using System.Web.Http.Results;
using AutoMapper;
using MyPortal.Controllers.Api;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.UnitTests.TestData;
using NUnit.Framework;

namespace MyPortal.UnitTests.ApiTests
{
    [TestFixture]
    public class ApiBasketItemsControllerTests
    {
        private BasketItemsController _controller;
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

            _controller = new BasketItemsController(_context);
        }
        
        [OneTimeTearDown]
        public void Clear()
        {
            _controller.Dispose();
            _context.Dispose();
        }

        [Test]
        public void GetBasketItems_ReturnsItems()
        {
            var student = _context.Students.SingleOrDefault(x => x.FirstName == "Aaron");
            
            Assert.IsNotNull(student);

            var result = _controller.GetBasketItems(student.Id).ToList();                       

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Art Pack", result.First().Product.Description);
        }

        [Test]
        public void GetTotal_ReturnsBasketTotal()
        {
            var student = _context.Students.SingleOrDefault(x => x.FirstName == "Aaron");
            
            Assert.IsNotNull(student);

            var result = _controller.GetTotal(student.Id);

            Assert.AreEqual(22.50m,result);
        }

        [Test]
        public void AddToBasket_AddsItemToBasket()
        {
            var student = _context.Students.SingleOrDefault(x => x.FirstName == "Dorothy");
            
            Assert.IsNotNull(student);

            var initial = _context.BasketItems.Count(x => x.StudentId == student.Id);

            var product = _context.Products.SingleOrDefault(x => x.Description == "Art Pack");
            
            Assert.IsNotNull(product);

            _controller.AddToBasket(new BasketItem {ProductId = product.Id, StudentId = student.Id});

            var result = _context.BasketItems.Count(x => x.StudentId == student.Id);
            
            Assert.AreEqual(initial + 1, result);
        }

        [Test]
        public void AddToBasket_StudentDoesNotExist_ReturnsNotFound()
        {
            var studentId = 9999;
            
            var product = _context.Products.SingleOrDefault(x => x.Description == "Art Pack");
            Assert.IsNotNull(product);
            
            var item = new BasketItem {StudentId = studentId, ProductId = product.Id};

            var actionResult = _controller.AddToBasket((item));

            var result = actionResult as NegotiatedContentResult<string>;
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NegotiatedContentResult<string>>(actionResult);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual("Student not found", result.Content);            
        }

        [Test]
        public void AddToBasket_ProductDoesNotExist_ReturnsNotFound()
        {
            var productId = 9999;

            var student = _context.Students.SingleOrDefault(x => x.FirstName == "Dorothy");
            Assert.IsNotNull(student);
            
            var item = new BasketItem {StudentId = student.Id, ProductId = productId};

            var actionResult = _controller.AddToBasket((item));

            var result = actionResult as NegotiatedContentResult<string>;
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NegotiatedContentResult<string>>(actionResult);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual("Product not found", result.Content);
        }

        [Test]
        public void AddToBasket_ProductNotAvailable_ReturnsBadRequest()
        {
            var product = _context.Products.SingleOrDefault(x => x.Description == "School Dinner");
            Assert.IsNotNull(product);

            var student = _context.Students.SingleOrDefault(x => x.FirstName == "Dorothy");
            Assert.IsNotNull(student);
            
            var item = new BasketItem {StudentId = student.Id, ProductId = product.Id};

            var actionResult = _controller.AddToBasket((item));

            var result = actionResult as NegotiatedContentResult<string>;
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NegotiatedContentResult<string>>(actionResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual("Product not available", result.Content);
        }

        [Test]
        public void AddToBasket_ProductOnceOnly_ReturnsBadRequest()
        {
            var product = _context.Products.SingleOrDefault(x => x.Description == "School Trip");
            Assert.IsNotNull(product);

            var student = _context.Students.SingleOrDefault(x => x.FirstName == "John");
            Assert.IsNotNull(student);
            
            var item = new BasketItem {ProductId = product.Id, StudentId = student.Id};

            var actionResult = _controller.AddToBasket((item));

            var result = actionResult as NegotiatedContentResult<string>;
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NegotiatedContentResult<string>>(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual("This product cannot be purchased more than once", result.Content);
        }

        [Test]
        public void RemoveFromBasket_RemovesItemFromBasket()
        {
            var student = _context.Students.SingleOrDefault(x => x.FirstName == "Aaron");
            
            Assert.IsNotNull(student);

            var initial = _context.BasketItems.Count(x => x.StudentId == student.Id);

            var item = _context.BasketItems.FirstOrDefault(x => x.StudentId == student.Id);
            
            Assert.IsNotNull(item);

            _controller.RemoveFromBasket(item.Id);

            var result = _context.BasketItems.Count(x => x.StudentId == student.Id);
            
            Assert.AreEqual(initial - 1, result); 
        }

        [Test]
        public void RemoveFromBasket_ItemDoesNotExist_ReturnsNotFound()
        {
            var itemId = 9999;

            var actionResult = _controller.RemoveFromBasket(itemId);

            var result = actionResult as NegotiatedContentResult<string>;
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NegotiatedContentResult<string>>(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual("Item not found", result.Content);
        }
    }
}

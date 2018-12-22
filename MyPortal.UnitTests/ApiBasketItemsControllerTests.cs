using System;
using System.Data.Common;
using System.Linq;
using AutoMapper;
using MyPortal.Controllers.Api;
using MyPortal.Models;
using Effort;
using MyPortal.Dtos;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace MyPortal.UnitTests
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
            DbConnection effortConnection = DbConnectionFactory.CreatePersistent("MyPortalDbContext");
            _context = new MyPortalDbContext(effortConnection);
            ContextControl.Populate(_context);
            ContextControl.InitialiseMaps();

            _controller = new BasketItemsController(_context);
        }

        [SetUp]
        public void Setup()
        {
            EffortProviderFactory.ResetDb();
        }

        [Test]
        public void GetBasketItems_ReturnsItems()
        {
            var studentId = _context.Students.SingleOrDefault(x => x.FirstName == "Aaron").Id;

            var result = _controller.GetBasketItems(studentId);

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Art Pack", result.First().Product.Description);
        }

        [Test]
        public void GetTotal_ReturnsBasketTotal()
        {
            var studentId = _context.Students.SingleOrDefault(x => x.FirstName == "Aaron").Id;

            var result = _controller.GetTotal(studentId);

            Assert.AreEqual(22.50m,result);
        }

        [Test]
        public void AddToBasket_AddsItemToBasket()
        {
            var studentId = _context.Students.SingleOrDefault(x => x.FirstName == "Dorothy").Id;

            var initial = _context.BasketItems.Count(x => x.StudentId == studentId);

            var product = _context.Products.SingleOrDefault(x => x.Description == "Art Pack");

            _controller.AddToBasket(new BasketItemDto() {ProductId = product.Id, StudentId = studentId});

            var result = _context.BasketItems.Count(x => x.StudentId == studentId);
            
            Assert.AreEqual(initial + 1, result);
        }

        [Test]
        public void RemoveFromBasket_RemovesItemFromBasket()
        {
            var studentId = _context.Students.SingleOrDefault(x => x.FirstName == "Aaron").Id;

            var initial = _context.BasketItems.Count(x => x.StudentId == studentId);

            var product = _context.Products.SingleOrDefault(x => x.Description == "Art Pack");

            var item = _context.BasketItems.FirstOrDefault(x => x.StudentId == studentId);

            _controller.RemoveFromBasket(item.Id);

            var result = _context.BasketItems.Count(x => x.StudentId == studentId);
            
            Assert.AreEqual(initial - 1, result); 
        }
    }
}

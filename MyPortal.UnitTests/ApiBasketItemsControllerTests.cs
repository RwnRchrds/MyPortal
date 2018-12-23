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

            var result = _controller.GetBasketItems(student.Id);           

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

            _controller.AddToBasket(new BasketItemDto() {ProductId = product.Id, StudentId = student.Id});

            var result = _context.BasketItems.Count(x => x.StudentId == student.Id);
            
            Assert.AreEqual(initial + 1, result);
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
    }
}

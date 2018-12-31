using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Effort;
using AutoMapper;
using MyPortal.Controllers.Api;
using MyPortal.Dtos;
using MyPortal.Models;
using NUnit.Framework;

namespace MyPortal.UnitTests
{
    [TestFixture]
    public class ApiProductsControllerTests
    {
        private ProductsController _controller;
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

            _controller = new ProductsController(_context);
        }
        
        [OneTimeTearDown]
        public void Clear()
        {
            _controller.Dispose();
            _context.Dispose();
        }

        [Test]
        public void GetProducts_ReturnsAllProducts()
        {
            var result = _controller.GetProducts();

            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public void GetProduct_ReturnsCorrectProduct()
        {
            var product = _context.Products.SingleOrDefault(x => x.Description == "School Trip");

            Assert.IsNotNull(product);

            var expected = Mapper.Map<Product, ProductDto>(product);

            var result = _controller.GetProduct(product.Id);

            Assert.IsNotNull(result);

            Assert.AreEqual(expected.Price, result.Price);
        }
    }
}

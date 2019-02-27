using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
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

            Assert.AreEqual(4, result.Count());
        }

        [Test]
        public void GetProduct_ReturnsProduct()
        {
            var product = _context.Products.SingleOrDefault(x => x.Description == "School Trip");

            Assert.IsNotNull(product);

            var expected = Mapper.Map<Product, ProductDto>(product);

            var result = _controller.GetProduct(product.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Price, result.Price);
        }

        [Test]
        public void NewProduct_CreatesProduct()
        {
            var newProduct = new Product {Description = "Test", Price = 0.99m, Visible = true, OnceOnly = false};

            var init = _context.Products.Count();

            _controller.NewProduct((newProduct));

            var result = _context.Products.Count();
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NegotiatedContentResult<string>>(result);
            Assert.AreEqual(init + 1, result);
        }

        [Test]
        public void UpdateProduct_UpdatesProduct()
        {
            var product = _context.Products.SingleOrDefault(x => x.Description == "Art Pack");
            
            Assert.IsNotNull(product);
            
            product.Price = 5000.00m;
            product.Description = "Art Learning Pack";
            product.OnceOnly = true;

            _controller.UpdateProduct((product));

            var result = _context.Products.SingleOrDefault(x => x.Id == product.Id);
            
            Assert.IsNotNull(result);      
            Assert.IsInstanceOf<OkNegotiatedContentResult<string>>(result);
            Assert.AreEqual(5000.00m, result.Price);
            Assert.AreEqual("Art Learning Pack", result.Description);
            Assert.AreEqual(true, result.OnceOnly);
        }

        [Test]
        public void DeleteProduct_RemovesProduct()
        {
            var init = _context.Products.Count();
            
            var product = _context.Products.SingleOrDefault(x => x.Description == "Delete Me");
            
            Assert.IsNotNull(product);

            _controller.DeleteProduct(product.Id);

            var result = _context.Products.Count();
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkNegotiatedContentResult<string>>(result);
            Assert.AreEqual(init - 1, result);
        }

        [Test]
        public void DeleteProduct_ProductDoesNotExist_ReturnsNotFound()
        {
            const int productId = 9999;

            var result = _controller.DeleteProduct(productId) as NegotiatedContentResult<string>;
            
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual("Product not found", result.Content);
        }
    }
}

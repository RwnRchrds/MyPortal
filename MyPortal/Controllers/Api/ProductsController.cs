using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    public class ProductsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public ProductsController()
        {
            _context = new MyPortalDbContext();
        }

        public ProductsController(MyPortalDbContext context)
        {
            _context = context;
        }

        //DELETE PRODUCT
        [HttpDelete]
        [Route("api/products/{id}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            var productInDb = _context.Products.SingleOrDefault(p => p.Id == id);

            if (productInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Product not found");
            }

            _context.Products.Remove(productInDb);
            _context.SaveChanges();

            return Ok("Product deleted");
        }

        //STORE: GET AVAILABLE PRODUCTS
        [HttpGet]
        [Route("api/products/store")]
        public IEnumerable<ProductDto> GetAvailableProducts(int student)
        {
            var purchased = _context.Sales.Where(a => a.StudentId == student);

            var inBasket = _context.BasketItems.Where(a => a.StudentId == student);

            return _context.Products
                .Where(x => !x.OnceOnly && x.Visible || x.Visible && purchased.All(p => p.ProductId != x.Id) &&
                            inBasket.All(b => b.ProductId != x.Id))
                .OrderBy(x => x.Description)
                .ToList()
                .Select(Mapper.Map<Product, ProductDto>);
        }

        //GET PRICE
        [HttpGet]
        [Route("api/products/price/{productId}")]
        public decimal GetPrice(int productId)
        {
            var productInDb = _context.Products.Single(x => x.Id == productId);

            if (productInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return productInDb.Price;
        }

        //GET SINGLE PRODUCT
        [HttpGet]
        [Route("api/products/{id}")]
        public ProductDto GetProduct(int id)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == id);

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<Product, ProductDto>(product);
        }

        //GET ALL PRODUCTS
        [HttpGet]
        [Route("api/products")]
        public IEnumerable<ProductDto> GetProducts()
        {
            return _context.Products
                .OrderBy(x => x.Description)
                .ToList()
                .Select(Mapper.Map<Product, ProductDto>);
        }

        //NEW PRODUCT
        [HttpPost]
        [Route("api/products/new")]
        public IHttpActionResult NewProduct(Product data)
        {
            var product = data;

            _context.Products.Add(product);
            _context.SaveChanges();

            return Ok("Product added");
        }

        //UPDATE PRODUCT
        [HttpPost]
        [Route("api/products/edit")]
        public IHttpActionResult UpdateProduct(Product product)
        {
            if (product == null)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid request data");
            }

            var productInDb = _context.Products.SingleOrDefault(x => x.Id == product.Id);

            if (productInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Product not found");
            }

            Mapper.Map(product, productInDb);
            productInDb.OnceOnly = product.OnceOnly;
            productInDb.Price = product.Price;
            productInDb.Visible = product.Visible;
            productInDb.Description = product.Description;

            _context.SaveChanges();

            return Ok("Product updated");
        }
    }
}
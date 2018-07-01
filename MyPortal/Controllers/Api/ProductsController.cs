using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    public class ProductsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public ProductsController()
        {
            _context = new MyPortalDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET PRODUCTS
        [HttpGet]
        [Route("api/products")]
        public IEnumerable<ProductDto> GetProducts()
        {
            return _context.Products
                .ToList()
                .Select(Mapper.Map<Product, ProductDto>);
        }

        //NEW PRODUCT
        [HttpPost]
        [Route("api/products/new")]
        public IHttpActionResult NewProduct(ProductDto data)
        {
            var product = Mapper.Map<ProductDto, Product>(data);

            _context.Products.Add(product);
            _context.SaveChanges();

            return Ok("Product added");
        }

        //DELETE PRODUCT
        [HttpDelete]
        [Route("api/products/{id}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            var productInDb = _context.Products.SingleOrDefault(p => p.Id == id);

            if (productInDb == null)
                return Content(HttpStatusCode.NotFound, "Product not found");

            _context.Products.Remove(productInDb);
            _context.SaveChanges();

            return Ok("Product deleted");
        }

        //STORE: GET AVAILABLE PRODUCTS
        [HttpGet]
        [Route("api/products/store")]
        public IEnumerable<ProductDto> GetAvailableProducts(int student)
        {
            var purchased = _context.Sales.Where(a => a.Student == student);

            var inBasket = _context.BasketItems.Where(a => a.Student == student);

            return _context.Products
                .Where(x => !x.OnceOnly && x.Visible || x.Visible && purchased.All(p => p.Product != x.Id) && inBasket.All(b => b.Product != x.Id))
                .ToList()
                .Select(Mapper.Map<Product, ProductDto>);
        }
    }
}

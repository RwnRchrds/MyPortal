using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public void NewProduct(ProductDto data)
        {
            var product = Mapper.Map<ProductDto, Product>(data);

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        //DELETE PRODUCT
        [HttpDelete]
        [Route("api/products/{id}")]
        public void DeleteProduct(int id)
        {
            var productInDb = _context.Products.SingleOrDefault(p => p.Id == id);

            if (productInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Products.Remove(productInDb);
            _context.SaveChanges();
        }
    }
}

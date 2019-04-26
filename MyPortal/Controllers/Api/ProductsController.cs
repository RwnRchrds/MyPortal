using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Database;

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

        /// <summary>
        /// Deletes the specified product from the database.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("api/products/{id}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            var productInDb = _context.FinanceProducts.SingleOrDefault(p => p.Id == id);

            if (productInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Product not found");
            }

            _context.FinanceProducts.Remove(productInDb);
            _context.SaveChanges();

            return Ok("Product deleted");
        }

        /// <summary>
        /// Gets the products available to buy for the specified student.
        /// </summary>
        /// <param name="student">The ID of the student to get products for.</param>
        /// <returns>Returns a list of DTOs of products available to buy for the specified student.</returns>
        [HttpGet]
        [Route("api/products/store")]
        public IEnumerable<FinanceProductDto> GetAvailableProducts(int student)
        {
            var purchased = _context.FinanceSales.Where(a => a.StudentId == student);

            var inBasket = _context.FinanceBasketItems.Where(a => a.StudentId == student);

            return _context.FinanceProducts
                .Where(x => !x.OnceOnly && x.Visible || x.Visible && purchased.All(p => p.ProductId != x.Id) &&
                            inBasket.All(b => b.ProductId != x.Id))
                .OrderBy(x => x.Description)
                .ToList()
                .Select(Mapper.Map<FinanceProduct, FinanceProductDto>);
        }

        /// <summary>
        /// Gets the price of the specified product.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <returns>Returns a decimal of the price of the product.</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        [Route("api/products/price/{productId}")]
        public decimal GetPrice(int productId)
        {
            var productInDb = _context.FinanceProducts.Single(x => x.Id == productId);

            if (productInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return productInDb.Price;
        }

        /// <summary>
        /// Get the specified product.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>Returns a DTO of the specified product</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        [Route("api/products/{id}")]
        public FinanceProductDto GetProduct(int id)
        {
            var product = _context.FinanceProducts.SingleOrDefault(x => x.Id == id);

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<FinanceProduct, FinanceProductDto>(product);
        }

        /// <summary>
        /// Gets a list of all products.
        /// </summary>
        /// <returns>Returns a list of DTOs of all products.</returns>
        [HttpGet]
        [Route("api/products")]
        public IEnumerable<FinanceProductDto> GetProducts()
        {
            return _context.FinanceProducts
                .OrderBy(x => x.Description)
                .ToList()
                .Select(Mapper.Map<FinanceProduct, FinanceProductDto>);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="data">The product to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/products/new")]
        public IHttpActionResult NewProduct(FinanceProduct data)
        {
            var product = data;

            _context.FinanceProducts.Add(product);
            _context.SaveChanges();

            return Ok("Product added");
        }

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/products/edit")]
        public IHttpActionResult UpdateProduct(FinanceProduct product)
        {
            if (product == null)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid request data");
            }

            var productInDb = _context.FinanceProducts.SingleOrDefault(x => x.Id == product.Id);

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
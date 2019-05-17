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
    public class BasketItemsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public BasketItemsController()
        {
            _context = new MyPortalDbContext();
        }

        public BasketItemsController(MyPortalDbContext context)
        {
            _context = context;
        }

/// <summary>
/// Adds a basket item to student's basket.
/// </summary>
/// <param name="data">The basket item to add to the database</param>
/// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/basket/add")]
        public IHttpActionResult AddToBasket(FinanceBasketItem data)
        {
            var studentQuery = _context.Students.SingleOrDefault(x => x.Id == data.StudentId);

            if (studentQuery == null)
            {
                return Content(HttpStatusCode.NotFound, "Student not found");
            }

            var productToAdd = _context.FinanceProducts.SingleOrDefault(x => x.Id == data.ProductId);

            if (productToAdd == null)
            {
                return Content(HttpStatusCode.NotFound, "Product not found");
            }

            if (!productToAdd.Visible)
            {
                return Content(HttpStatusCode.BadRequest, "Product not available");
            }

            var purchased =
                _context.FinanceSales.Where(x =>
                    x.StudentId == data.StudentId && x.ProductId == data.ProductId && x.FinanceProduct.OnceOnly);

            var inBasket =
                _context.FinanceBasketItems.Where(x =>
                    x.StudentId == data.StudentId && x.ProductId == data.ProductId && x.FinanceProduct.OnceOnly);

            if (purchased.Any() || inBasket.Any())
            {
                return Content(HttpStatusCode.BadRequest, "This product cannot be purchased more than once");
            }

            var itemToAdd = new FinanceBasketItem
            {
                ProductId = data.ProductId,
                StudentId = data.StudentId
            };

            _context.FinanceBasketItems.Add(itemToAdd);
            _context.SaveChanges();

            return Ok("Item added to basket");
        }

/// <summary>
/// Gets a list of basket items for the specified student.
/// </summary>
/// <param name="student">The ID of the student to fetch basket items for.</param>
/// <returns>Returns a list of DTOs of basket items for the student.</returns>
        [HttpGet]
        [Route("api/basket")]
        public IEnumerable<FinanceBasketItemDto> GetBasketItems(int student)
        {
            return _context.FinanceBasketItems
                .Where(x => x.StudentId == student)
                .OrderBy(x => x.FinanceProduct.Description)
                .ToList()
                .Select(Mapper.Map<FinanceBasketItem, FinanceBasketItemDto>);
        }

/// <summary>
/// Gets the total price of all the items in the specified student's basket.
/// </summary>
/// <param name="student"></param>
/// <returns>Returns a decimal of the total price.</returns>
        [HttpGet]
        [Route("api/basket/total")]
        public decimal GetTotal(int student)
        {
            var allItems = _context.FinanceBasketItems.Where(x => x.StudentId == student);

            if (!allItems.Any())
            {
                return 0.00m;
            }

            var total = allItems.Sum(x => x.FinanceProduct.Price);

            return total;
        }

/// <summary>
/// Removes a basket item from the student's basket.
/// </summary>
/// <param name="id">The ID of the item to remove from the basket.</param>
/// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("api/basket/remove/{id}")]
        public IHttpActionResult RemoveFromBasket(int id)
        {
            var itemInDb = _context.FinanceBasketItems.SingleOrDefault(x => x.Id == id);

            if (itemInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Item not found");
            }

            _context.FinanceBasketItems.Remove(itemInDb);
            _context.SaveChanges();

            return Ok("Item removed from basket");
        }
    }
}
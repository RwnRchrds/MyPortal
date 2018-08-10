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
    public class BasketItemsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public BasketItemsController()
        {
            _context = new MyPortalDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET BASKET ITEMS
        [HttpGet]
        [Route("api/basket")]
        public IEnumerable<BasketItemDto> GetBasketItems(int student)
        {
            return _context.BasketItems
                .Where(x => x.Student == student)
                .OrderBy(x => x.Product1.Description)
                .ToList()
                .Select(Mapper.Map<BasketItem, BasketItemDto>);
        }

        //GET BASKET TOTAL
        [HttpGet]
        [Route("api/basket/total")]
        public decimal GetTotal(int student)
        {
            var allItems = _context.BasketItems.Where(x => x.Student == student);

            if (!allItems.Any())
                return 0.00m;

            var total = allItems.Sum(x => x.Product1.Price);

            return total;
        }

        //ADD TO BASKET
        [HttpPost]
        [Route("api/basket/add")]
        public IHttpActionResult AddToBasket(BasketItemDto data)
        {
            var studentQuery = _context.Students.SingleOrDefault(x => x.Id == data.Student);

            if (studentQuery == null)
                return Content(HttpStatusCode.NotFound, "Student not found");

            var productToAdd = _context.Products.SingleOrDefault(x => x.Id == data.Product);

            if (productToAdd == null)
                return Content(HttpStatusCode.NotFound, "Product not found");

            if (!productToAdd.Visible)
                return Content(HttpStatusCode.BadRequest, "Product not available");

            var purchased =
                _context.Sales.Where(x =>
                    x.Student == data.Student && x.Product == data.Product && x.Product1.OnceOnly);

            var inBasket =
                _context.BasketItems.Where(x =>
                    x.Student == data.Student && x.Product == data.Product && x.Product1.OnceOnly);

            if (purchased.Any() || inBasket.Any())
                return Content(HttpStatusCode.BadRequest, "This item cannot be purchased more than once");

            var itemToAdd = new BasketItem
            {
                Product = data.Product,
                Student = data.Student
            };

            _context.BasketItems.Add(itemToAdd);
            _context.SaveChanges();

            return Ok("Item added to basket");
        }

        //REMOVE FROM BASKET
        [HttpDelete]
        [Route("api/basket/remove/{id}")]
        public IHttpActionResult RemoveFromBasket(int id)
        {
            var itemInDb = _context.BasketItems.SingleOrDefault(x => x.Id == id);

            if (itemInDb == null)
                return Content(HttpStatusCode.NotFound, "Item not found");

            _context.BasketItems.Remove(itemInDb);
            _context.SaveChanges();

            return Ok("Item removed from basket");
        }
    }
}
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

        public BasketItemsController(MyPortalDbContext context)
        {
            _context = context;
        }

        //ADD TO BASKET
        [HttpPost]
        [Route("api/basket/add")]
        public IHttpActionResult AddToBasket(BasketItemDto data)
        {
            var studentQuery = _context.Students.SingleOrDefault(x => x.Id == data.StudentId);

            if (studentQuery == null)
                return Content(HttpStatusCode.NotFound, "Student not found");

            var productToAdd = _context.Products.SingleOrDefault(x => x.Id == data.ProductId);

            if (productToAdd == null)
                return Content(HttpStatusCode.NotFound, "Product not found");

            if (!productToAdd.Visible)
                return Content(HttpStatusCode.BadRequest, "Product not available");

            var purchased =
                _context.Sales.Where(x =>
                    x.StudentId == data.StudentId && x.ProductId == data.ProductId && x.Product.OnceOnly);

            var inBasket =
                _context.BasketItems.Where(x =>
                    x.StudentId == data.StudentId && x.ProductId == data.ProductId && x.Product.OnceOnly);

            if (purchased.Any() || inBasket.Any())
                return Content(HttpStatusCode.BadRequest, "This product cannot be purchased more than once");

            var itemToAdd = new BasketItem
            {
                ProductId = data.ProductId,
                StudentId = data.StudentId
            };

            _context.BasketItems.Add(itemToAdd);
            _context.SaveChanges();

            return Ok("Item added to basket");
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
                .Where(x => x.StudentId == student)
                .OrderBy(x => x.Product.Description)
                .ToList()
                .Select(Mapper.Map<BasketItem, BasketItemDto>);
        }

        //GET BASKET TOTAL
        [HttpGet]
        [Route("api/basket/total")]
        public decimal GetTotal(int student)
        {
            var allItems = _context.BasketItems.Where(x => x.StudentId == student);

            if (!allItems.Any())
                return 0.00m;

            var total = allItems.Sum(x => x.Product.Price);

            return total;
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
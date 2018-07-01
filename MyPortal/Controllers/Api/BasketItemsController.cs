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
                .ToList()
                .Select(Mapper.Map<BasketItem, BasketItemDto>);
        }

        //ADD TO BASKET
        [HttpPost]
        [Route("api/basket/add")]
        public IHttpActionResult AddToBasket(int product, int student)
        {
            var studentQuery = _context.Students.SingleOrDefault(x => x.Id == student);

            if (studentQuery == null)
                return NotFound();

            var productToAdd = _context.Products.SingleOrDefault(x => x.Id == product);

            if (productToAdd == null)
                return NotFound();

            if (!productToAdd.Visible)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var purchased = _context.Sales.Where(x => x.Student == student && x.Product == product && x.Product1.OnceOnly);

            var inBasket = _context.BasketItems.Where(x => x.Student == student && x.Product == product && x.Product1.OnceOnly);

            if (purchased.Any() || inBasket.Any())
                return Content(HttpStatusCode.BadRequest,"This item cannot be purchased more than once");

            var itemToAdd = new BasketItem()
            {
                Product = product,
                Student = student
            };

            _context.BasketItems.Add(itemToAdd);
            _context.SaveChanges();

            return Ok("Item added to basket");
        }

        //REMOVE FROM BASKET
        [HttpDelete]
        [Route("api/basket/remove")]
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

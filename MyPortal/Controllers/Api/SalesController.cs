using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Misc;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    public class SalesController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public SalesController()
        {
            _context = new MyPortalDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("api/sales")]
        public IEnumerable<SaleDto> GetSales()
        {
            return _context.Sales
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<Sale, SaleDto>);
        }

        //DELETE SALE
        [HttpDelete]
        [Route("api/sales/delete/{id}")]
        public IHttpActionResult DeleteSale(int id)
        {
            var saleInDb = _context.Sales.SingleOrDefault(p => p.Id == id);

            if (saleInDb == null)
                return Content(HttpStatusCode.NotFound, "Sale not found");

            _context.Sales.Remove(saleInDb);
            _context.SaveChanges();

            return Ok("Sale deleted");
        }

        //REFUND SALE
        [HttpDelete]
        [Route("api/sales/refund/{id}")]
        public IHttpActionResult RefundSale(int id)
        {
            var saleInDb = _context.Sales.SingleOrDefault(p => p.Id == id);

            if (saleInDb == null)
                return Content(HttpStatusCode.NotFound, "Sale not found");

            var amount = saleInDb.Product1.Price;

            var student = saleInDb.Student1;

            student.AccountBalance += amount;

            _context.Sales.Remove(saleInDb);
            _context.SaveChanges();

            return Ok("Sale refunded");
        }

        //Processes a Sale for ONE Product
        public void NewSale(SaleDto sale)
        {
            var student = _context.Students.SingleOrDefault(x => x.Id == sale.Student);

            var product = _context.Products.SingleOrDefault(x => x.Id == sale.Product);

            if (student == null || product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            /*if (product.Price > student.AccountBalance)
                throw new HttpResponseException(HttpStatusCode.BadRequest);*/

            student.AccountBalance -= product.Price;

            _context.Sales.Add(Mapper.Map<SaleDto, Sale>(sale));
        }

        //STORE: NEW PURCHASE (From Student Side)
        [HttpPost]
        [Route("api/sales/purchase")]
        public IHttpActionResult Purchase(Checkout data)
        {
            //Check student actually exists
            var student = _context.Students.SingleOrDefault(x => x.Id == data.studentId);

            if (student == null)
                return Content(HttpStatusCode.NotFound, "Student not found");

            //Obtain items from student's shopping basket
            var basket = _context.BasketItems.Where(x => x.Student == data.studentId);

            //Check there are actually items in the basket
            if (!basket.Any())
                return Content(HttpStatusCode.BadRequest, "There are no items in your basket");

            //Check student has enough money to afford all items
            var totalCost = basket.Sum(x => x.Product1.Price);

            if (totalCost > student.AccountBalance)
                return Content(HttpStatusCode.BadRequest, "Insufficient Funds");

            //Process sales for each item
            foreach (var item in basket)
            {
                var sale = new SaleDto
                {
                    Student = data.studentId,
                    Product = item.Product,
                    Date = DateTime.Today
                };

                NewSale(sale);
            }

            //Remove items from student's basket once transaction has completed
            _context.BasketItems.RemoveRange(basket);

            _context.SaveChanges();

            return Ok("Purchase completed");
        }
    }
}
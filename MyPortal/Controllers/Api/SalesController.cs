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

        //TEST BALANCE
        [HttpPost]
        [Route("api/sales/query")]
        public bool AssessBalance(SaleDto sale)
        {
            var productToQuery = _context.Products.SingleOrDefault(x => x.Id == sale.ProductId);

            var studentToQuery = _context.Students.SingleOrDefault(x => x.Id == sale.StudentId);

            if (productToQuery == null || studentToQuery == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return studentToQuery.AccountBalance >= productToQuery.Price;
        }

        //DELETE SALE
        [HttpDelete]
        [Route("api/sales/delete/{id}")]
        public IHttpActionResult DeleteSale(int id)
        {
            var saleInDb = _context.Sales.SingleOrDefault(p => p.Id == id);

            if (saleInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Sale not found");
            }

            _context.Sales.Remove(saleInDb);
            _context.SaveChanges();

            return Ok("Sale deleted");
        }


        //GET UNPROCESSED SALES
        [HttpGet]
        [Route("api/sales/pending")]
        public IEnumerable<SaleDto> GetPendingSales()
        {
            return _context.Sales.Where(x => x.Processed == false)
                .ToList()
                .Select(Mapper.Map<Sale, SaleDto>);
        }

        //GET ALL SALES
        [HttpGet]
        [Route("api/sales/all")]
        public IEnumerable<SaleDto> GetSales()
        {
            return _context.Sales
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<Sale, SaleDto>);
        }

        //GET SALE FOR STUDENT
        [HttpGet]
        [Route("api/sales/student")]
        public IEnumerable<SaleDto> GetSalesForStudent(int studentId)
        {
            return _context.Sales
                .Where(x => x.StudentId == studentId)
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<Sale, SaleDto>);
        }

        //GET UNPROCESSED SALES
        [HttpGet]
        [Route("api/sales")]
        public IEnumerable<SaleDto> GetUnprocessedSales()
        {
            return _context.Sales
                .Where(x => x.Processed == false)
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<Sale, SaleDto>);
        }

        //Processes a Sale for ONE Product
        public void InvokeSale(SaleDto sale)
        {
            var student = _context.Students.SingleOrDefault(x => x.Id == sale.StudentId);

            var product = _context.Products.SingleOrDefault(x => x.Id == sale.ProductId);

            if (student == null || product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            /*if (product.Price > student.AccountBalance)
                throw new HttpResponseException(HttpStatusCode.BadRequest);*/

            student.AccountBalance -= product.Price;

            sale.AmountPaid = product.Price;

            _context.Sales.Add(Mapper.Map<SaleDto, Sale>(sale));
        }

        //MARK SALE AS PROCESSED
        [HttpPost]
        [Route("api/sales/complete/{id}")]
        public IHttpActionResult MarkSaleProcessed(int id)
        {
            var saleInDb = _context.Sales.Single(x => x.Id == id);

            if (saleInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Sale not found");
            }

            if (saleInDb.Processed)
            {
                return Content(HttpStatusCode.BadRequest, "Sale already marked as processed");
            }

            saleInDb.Processed = true;

            _context.SaveChanges();

            return Ok("Sale marked as processed");
        }

        //NEW SALE
        [HttpPost]
        [Route("api/sales/new")]
        public IHttpActionResult NewSale(Sale sale)
        {
            sale.Date = DateTime.Now;

            sale.Processed = true;

            var student = _context.Students.SingleOrDefault(x => x.Id == sale.StudentId);

            var product = _context.Products.SingleOrDefault(x => x.Id == sale.ProductId);

            if (student == null)
            {
                return Content(HttpStatusCode.NotFound, "Student not found");
            }

            if (product == null)
            {
                return Content(HttpStatusCode.NotFound, "Product not found");
            }

            student.AccountBalance -= product.Price;

            sale.AmountPaid = product.Price;

            _context.Sales.Add(sale);
            _context.SaveChanges();

            return Ok("Sale completed");
        }

        //STORE: NEW PURCHASE (From Student Side)
        [HttpPost]
        [Route("api/sales/purchase")]
        public IHttpActionResult Purchase(Checkout data)
        {
            //Check student actually exists
            var student = _context.Students.SingleOrDefault(x => x.Id == data.StudentId);

            if (student == null)
            {
                return Content(HttpStatusCode.NotFound, "Student not found");
            }


            //Obtain items from student's shopping basket
            var basket = _context.BasketItems.Where(x => x.StudentId == data.StudentId);

            //Check there are actually items in the basket
            if (!basket.Any())
            {
                return Content(HttpStatusCode.BadRequest, "There are no items in your basket");
            }

            //Check student has enough money to afford all items
            var totalCost = basket.Sum(x => x.Product.Price);

            if (totalCost > student.AccountBalance)
            {
                return Content(HttpStatusCode.BadRequest, "Insufficient funds");
            }

            //Process sales for each item
            foreach (var item in basket)
            {
                var sale = new SaleDto
                {
                    StudentId = data.StudentId,
                    ProductId = item.ProductId,
                    Date = DateTime.Today
                };

                InvokeSale(sale);
            }

            //Remove items from student's basket once transaction has completed
            _context.BasketItems.RemoveRange(basket);

            _context.SaveChanges();

            return Ok("Purchase completed");
        }

        //REFUND SALE
        [HttpDelete]
        [Route("api/sales/refund/{id}")]
        public IHttpActionResult RefundSale(int id)
        {
            var saleInDb = _context.Sales.SingleOrDefault(p => p.Id == id);

            if (saleInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Sale not found");
            }

            var amount = saleInDb.AmountPaid;

            var student = saleInDb.Student;

            student.AccountBalance += amount;

            _context.Sales.Remove(saleInDb);
            _context.SaveChanges();

            return Ok("Sale refunded");
        }
    }
}
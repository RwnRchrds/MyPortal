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

        /// <summary>
        /// Checks whether the student has enough funds to purchase an item.
        /// </summary>
        /// <param name="sale">The sale with the</param>
        /// <returns>Returns a boolean value indicating whether the student has enough funds to purchase the item.</returns>
        /// <exception cref="HttpResponseException">Thrown when the product or student is not found.</exception>
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

        /// <summary>
        /// Deletes the specified sale.
        /// </summary>
        /// <param name="id">The ID of the sale to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
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


        /// <summary>
        /// Gets a list of sales that have not been marked as completed.
        /// </summary>
        /// <returns>Returns a list of DTOs of sales that have not been marked as completed.</returns>
        [HttpGet]
        [Route("api/sales/pending")]
        public IEnumerable<SaleDto> GetPendingSales()
        {
            return _context.Sales.Where(x => x.Processed == false)
                .ToList()
                .Select(Mapper.Map<Sale, SaleDto>);
        }

        /// <summary>
        /// Gets a list of all sales
        /// </summary>
        /// <returns>Returns a list of DTOs of all sales.</returns>
        [HttpGet]
        [Route("api/sales/all")]
        public IEnumerable<SaleDto> GetSales()
        {
            return _context.Sales
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<Sale, SaleDto>);
        }

        /// <summary>
        /// Gets a list of sales for a particular student.
        /// </summary>
        /// <param name="studentId">The ID of the student to fetch sales for.</param>
        /// <returns>Returns a list of DTOs of sales for the specified student</returns>
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

        /// <summary>
        /// Gets a list of sales that have not been marked as completed.
        /// </summary>
        /// <returns>Returns a list of DTOs of sales that have not been marked as completed.</returns>
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

        /// <summary>
        /// Creates a sale for one product.
        /// </summary>
        /// <param name="sale">The sale to create.</param>
        /// <exception cref="HttpResponseException">Thrown if the student or product is not found.</exception>
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

        /// <summary>
        /// Marks a sale as processed (completed).
        /// </summary>
        /// <param name="id">The ID of the sale to mark as processed.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
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

        /// <summary>
        /// Creates a sale.
        /// </summary>
        /// <param name="sale">The sale to create.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
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

        /// <summary>
        /// Processes a purchase made by a student using the online store.
        /// </summary>
        /// <param name="data">The checkout object to process the sale for.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
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

        /// <summary>
        /// Refunds the specified sale to the student.
        /// </summary>
        /// <param name="id">The ID of the sale to refund.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
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
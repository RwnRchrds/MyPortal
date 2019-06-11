using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using MyPortal.Processes;

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
        public bool AssessBalance(FinanceSaleDto sale)
        {
            var productToQuery = _context.FinanceProducts.SingleOrDefault(x => x.Id == sale.ProductId);

            var studentToQuery = _context.Students.SingleOrDefault(x => x.Id == sale.StudentId);

            if (productToQuery == null || studentToQuery == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (studentToQuery.FreeSchoolMeals && productToQuery.FinanceProductType.IsMeal)
            {
                return true;
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
            var saleInDb = _context.FinanceSales.SingleOrDefault(p => p.Id == id);

            if (saleInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Sale not found");
            }

            _context.FinanceSales.Remove(saleInDb);
            _context.SaveChanges();

            return Ok("Sale deleted");
        }


        /// <summary>
        /// Gets a list of sales that have not been marked as completed.
        /// </summary>
        /// <returns>Returns a list of DTOs of sales that have not been marked as completed.</returns>
        [HttpGet]
        [Route("api/sales/processed")]
        public IEnumerable<FinanceSaleDto> GetPendingSales()
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return _context.FinanceSales.Where(x => x.Processed && x.AcademicYearId == academicYearId)
                .ToList()
                .Select(Mapper.Map<FinanceSale, FinanceSaleDto>);
        }

        /// <summary>
        /// Gets a list of all sales
        /// </summary>
        /// <returns>Returns a list of DTOs of all sales.</returns>
        [HttpGet]
        [Route("api/sales/all")]
        public IEnumerable<FinanceSaleDto> GetSales()
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return _context.FinanceSales
                .Where(x => x.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<FinanceSale, FinanceSaleDto>);
        }

        /// <summary>
        /// Gets a list of sales for a particular student.
        /// </summary>
        /// <param name="studentId">The ID of the student to fetch sales for.</param>
        /// <returns>Returns a list of DTOs of sales for the specified student</returns>
        [HttpGet]
        [Route("api/sales/student")]
        public IEnumerable<FinanceSaleDto> GetSalesForStudent(int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return _context.FinanceSales
                .Where(x => x.StudentId == studentId && x.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<FinanceSale, FinanceSaleDto>);
        }

        /// <summary>
        /// Gets a list of sales that have not been marked as completed.
        /// </summary>
        /// <returns>Returns a list of DTOs of sales that have not been marked as completed.</returns>
        [HttpGet]
        [Route("api/sales")]
        public IEnumerable<FinanceSaleDto> GetUnprocessedSales()
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return _context.FinanceSales
                .Where(x => x.Processed == false && x.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<FinanceSale, FinanceSaleDto>);
        }

        /// <summary>
        /// Creates a sale for one product.
        /// </summary>
        /// <param name="sale">The sale to create.</param>
        /// <exception cref="HttpResponseException">Thrown if the student or product is not found.</exception>
        public void InvokeSale(FinanceSale sale)
        {            
            var student = _context.Students.SingleOrDefault(x => x.Id == sale.StudentId);

            var product = _context.FinanceProducts.SingleOrDefault(x => x.Id == sale.ProductId);

            if (student == null || product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            /*if (product.Price > student.AccountBalance)
                throw new HttpResponseException(HttpStatusCode.BadRequest);*/

            student.AccountBalance -= product.Price;

            sale.AmountPaid = product.Price;            

            _context.FinanceSales.Add(sale);
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
            var saleInDb = _context.FinanceSales.Single(x => x.Id == id);

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
        public IHttpActionResult NewSale(FinanceSale sale)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            sale.Date = DateTime.Now;

            sale.Processed = true;

            var student = _context.Students.SingleOrDefault(x => x.Id == sale.StudentId);

            var product = _context.FinanceProducts.SingleOrDefault(x => x.Id == sale.ProductId);

            if (student == null)
            {
                return Content(HttpStatusCode.NotFound, "Student not found");
            }

            if (product == null)
            {
                return Content(HttpStatusCode.NotFound, "Product not found");
            }

            if (student.FreeSchoolMeals && product.FinanceProductType.IsMeal)
            {
                sale.AmountPaid = 0.00m;
                sale.AcademicYearId = academicYearId;

                _context.FinanceSales.Add(sale);
                _context.SaveChanges();

                return Ok("Sale completed");
            }

            student.AccountBalance -= product.Price;

            sale.AmountPaid = product.Price;
            sale.AcademicYearId = academicYearId;

            _context.FinanceSales.Add(sale);
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
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            if (User.IsInRole("Student"))
            {
                new StudentsController().AuthenticateStudentRequest(data.StudentId);
            }

            //Check student actually exists
            var student = _context.Students.SingleOrDefault(x => x.Id == data.StudentId);

            if (student == null)
            {
                return Content(HttpStatusCode.NotFound, "Student not found");
            }


            //Obtain items from student's shopping basket
            var basket = _context.FinanceBasketItems.Where(x => x.StudentId == data.StudentId);

            //Check there are actually items in the basket
            if (!basket.Any())
            {
                return Content(HttpStatusCode.BadRequest, "There are no items in your basket");
            }

            //Check student has enough money to afford all items
            var totalCost = basket.Sum(x => x.FinanceProduct.Price);

            if (totalCost > student.AccountBalance)
            {
                return Content(HttpStatusCode.BadRequest, "Insufficient funds");
            }

            //Process sales for each item
            foreach (var item in basket)
            {
                var sale = new FinanceSale
                {
                    StudentId = data.StudentId,
                    ProductId = item.ProductId,
                    Date = DateTime.Today,
                    AcademicYearId = academicYearId
                };

                InvokeSale(sale);
            }

            //Remove items from student's basket once transaction has completed
            _context.FinanceBasketItems.RemoveRange(basket);

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
            var saleInDb = _context.FinanceSales.SingleOrDefault(p => p.Id == id);

            if (saleInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Sale not found");
            }

            var amount = saleInDb.AmountPaid;

            var student = saleInDb.CoreStudent;

            student.AccountBalance += amount;

            _context.FinanceSales.Remove(saleInDb);
            _context.SaveChanges();

            return Ok("Sale refunded");
        }
    }
}
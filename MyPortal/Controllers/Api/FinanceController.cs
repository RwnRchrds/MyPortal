using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using MyPortal.Processes;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/finance")]
    public class FinanceController : MyPortalApiController
    {
        /// <summary>
        ///     Adds a basket item to student's basket.
        /// </summary>
        /// <param name="basketItem">The basket item to add to the database</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("basketItems/create")]
        public IHttpActionResult CreateBasketItem([FromBody] FinanceBasketItem basketItem)
        {
            return PrepareResponse(FinanceProcesses.CreateBasketItem(basketItem, _context));
        }

        /// <summary>
        ///     Gets a list of basket items for the specified student.
        /// </summary>
        /// <param name="studentId">The ID of the student to fetch basket items for.</param>
        /// <returns>Returns a list of DTOs of basket items for the student.</returns>
        [HttpGet]
        [Route("basket/{studentId:int}")]
        public IEnumerable<FinanceBasketItemDto> GetBasketItemsForStudent([FromUri] int studentId)
        {
            return PrepareResponseObject(FinanceProcesses.GetBasketItemsForStudent(studentId, _context));
        }

        /// <summary>
        ///     Gets the total price of all the items in the specified student's basket.
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>Returns a decimal of the total price.</returns>
        [HttpGet]
        [Route("basket/total/{studentId:int}")]
        public decimal GetTotal([FromUri] int studentId)
        {
            return PrepareResponseObject(FinanceProcesses.GetBasketTotalForStudent(studentId, _context));
        }

        /// <summary>
        ///     Removes a basket item from the student's basket.
        /// </summary>
        /// <param name="basketItemId">The ID of the item to remove from the basket.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("basket/remove/{basketItemId:int}")]
        public IHttpActionResult RemoveFromBasket([FromUri] int basketItemId)
        {
            return PrepareResponse(FinanceProcesses.DeleteBasketItem(basketItemId, _context));
        }

        [HttpDelete]
        [Route("products/delete/{productId:int}")]
        public IHttpActionResult DeleteProduct([FromUri] int productId)
        {
            return PrepareResponse(FinanceProcesses.DeleteProduct(productId, _context));
        }

        /// <summary>
        ///     Gets the products available to buy for the specified student.
        /// </summary>
        /// <param name="studentId">The ID of the student to get products for.</param>
        /// <returns>Returns a list of DTOs of products available to buy for the specified student.</returns>
        [HttpGet]
        [Route("products/get/available/{studentId:int}")]
        public IEnumerable<FinanceProductDto> GetAvailableProductsForStudent([FromUri] int studentId)
        {
            return PrepareResponseObject(FinanceProcesses.GetAvailableProductsForStudent(studentId, _context));
        }

        /// <summary>
        ///     Gets the price of the specified product.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <returns>Returns a decimal of the price of the product.</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        [Route("products/price/{productId:int}")]
        public decimal GetProductPrice([FromUri] int productId)
        {
            return PrepareResponseObject(FinanceProcesses.GetProductPrice(productId, _context));
        }

        /// <summary>
        ///     Get the specified product.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <returns>Returns a DTO of the specified product</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        [Route("products/get/byId/{productId:int}")]
        public FinanceProductDto GetProductById([FromUri] int productId)
        {
            return PrepareResponseObject(FinanceProcesses.GetProductById(productId, _context));
        }

        [HttpGet]
        [Route("products/get/all")]
        public IEnumerable<FinanceProductDto> GetAllProducts()
        {
            return PrepareResponseObject(FinanceProcesses.GetAllProducts(_context));
        }

        [HttpPost]
        [Route("products/get/dataGrid/all")]
        public IHttpActionResult GetAllProductsForDataGrid([FromBody] DataManagerRequest dm)
        {
            var products = PrepareResponseObject(FinanceProcesses.GetAllProducts_DataGrid(_context));

            return PrepareDataGridObject(products, dm);
        }

        /// <summary>
        ///     Creates a new product.
        /// </summary>
        /// <param name="product">The product to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("products/create")]
        public IHttpActionResult NewProduct([FromBody] FinanceProduct product)
        {
            return PrepareResponse(FinanceProcesses.CreateProduct(product, _context));
        }

        /// <summary>
        ///     Updates the specified product.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("products/edit")]
        public IHttpActionResult UpdateProduct([FromBody] FinanceProduct product)
        {
            return PrepareResponse(FinanceProcesses.UpdateProduct(product, _context));
        }

        /// <summary>
        ///     Checks whether the student has enough funds to purchase an item.
        /// </summary>
        /// <param name="sale">The sale with the</param>
        /// <returns>Returns a boolean value indicating whether the student has enough funds to purchase the item.</returns>
        /// <exception cref="HttpResponseException">Thrown when the product or student is not found.</exception>
        [HttpPost]
        [Route("sales/queryBalance")]
        public bool AssessBalance([FromBody] FinanceSale sale)
        {
            return PrepareResponseObject(FinanceProcesses.AssessBalance(sale, _context));
        }

        /// <summary>
        ///     Deletes the specified sale.
        /// </summary>
        /// <param name="saleId">The ID of the sale to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("sales/delete/{saleId:int}")]
        public IHttpActionResult DeleteSale([FromUri] int saleId)
        {
            return PrepareResponse(FinanceProcesses.DeleteSale(saleId, _context));
        }


        /// <summary>
        ///     Gets a list of sales that have not been marked as completed.
        /// </summary>
        /// <returns>Returns a list of DTOs of sales that have not been marked as completed.</returns>
        [HttpGet]
        [Route("sales/get/processed")]
        public IEnumerable<FinanceSaleDto> GetProcessedSales()
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponseObject(FinanceProcesses.GetProcessedSales(academicYearId, _context));
        }

        [HttpPost]
        [Route("sales/get/dataGrid/processed")]
        public IHttpActionResult GetProcessedSalesForDataGrid([FromBody] DataManagerRequest dm)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var sales = PrepareResponseObject(FinanceProcesses.GetProcessedSales_DataGrid(academicYearId, _context));

            return PrepareDataGridObject(sales, dm);
        }

        /// <summary>
        ///     Gets a list of all sales
        /// </summary>
        /// <returns>Returns a list of DTOs of all sales.</returns>
        [HttpGet]
        [Route("sales/get/all")]
        public IEnumerable<FinanceSaleDto> GetAllSales()
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponseObject(FinanceProcesses.GetAllSales(academicYearId, _context));
        }

        [HttpPost]
        [Route("sales/get/dataGrid/all")]
        public IHttpActionResult GetAllSalesForDataGrid([FromBody] DataManagerRequest dm)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var sales = PrepareResponseObject(FinanceProcesses.GetAllSales_DataGrid(academicYearId, _context));

            return PrepareDataGridObject(sales, dm);
        }

        /// <summary>
        ///     Gets a list of sales for a particular student.
        /// </summary>
        /// <param name="studentId">The ID of the student to fetch sales for.</param>
        /// <returns>Returns a list of DTOs of sales for the specified student</returns>
        [HttpGet]
        [Route("sales/get/byStudent/{studentId:int}")]
        public IEnumerable<FinanceSaleDto> GetSalesForStudent([FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponseObject(FinanceProcesses.GetAllSalesForStudent(studentId, academicYearId, _context));
        }

        /// <summary>
        ///     Gets a list of sales that have not been marked as completed.
        /// </summary>
        /// <returns>Returns a list of DTOs of sales that have not been marked as completed.</returns>
        [HttpGet]
        [Route("sales/get/pending")]
        public IEnumerable<FinanceSaleDto> GetPendingSales()
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponseObject(FinanceProcesses.GetPendingSales(academicYearId, _context));
        }

        [HttpPost]
        [Route("sales/get/dataGrid/pending")]
        public IHttpActionResult GetPendingSalesForDataGrid([FromBody] DataManagerRequest dm)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var sales = PrepareResponseObject(FinanceProcesses.GetPendingSales_DataGrid(academicYearId, _context));

            return PrepareDataGridObject(sales, dm);
        }

        /// <summary>
        ///     Marks a sale as processed (completed).
        /// </summary>
        /// <param name="saleId">The ID of the sale to mark as processed.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("sales/markComplete/{saleId:int}")]
        public IHttpActionResult MarkSaleProcessed([FromUri] int saleId)
        {
            var saleInDb = _context.FinanceSales.Single(x => x.Id == saleId);

            if (saleInDb == null) return Content(HttpStatusCode.NotFound, "Sale not found");

            if (saleInDb.Processed) return Content(HttpStatusCode.BadRequest, "Sale already marked as processed");

            saleInDb.Processed = true;

            _context.SaveChanges();

            return Ok("Sale marked as processed");
        }

        /// <summary>
        ///     Creates a sale.
        /// </summary>
        /// <param name="sale">The sale to create.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("sales/create")]
        public IHttpActionResult CreateSale([FromBody] FinanceSale sale)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponse(FinanceProcesses.CreateSale(sale, academicYearId, _context));
        }

        /// <summary>
        ///     Processes a purchase made by a student using the online store.
        /// </summary>
        /// <param name="studentId">The checkout object to process the sale for.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("sales/checkoutBasket/{studentId:int}")]
        public IHttpActionResult Purchase([FromBody] int studentId)
        {
            AuthenticateStudentRequest(studentId);
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponse(FinanceProcesses.CheckoutBasketForStudent(studentId, academicYearId, _context));
        }

        /// <summary>
        ///     Refunds the specified sale to the student.
        /// </summary>
        /// <param name="saleId">The ID of the sale to refund.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        
        [HttpPost]
        [Route("sales/refund/{saleId:int}")]
        public IHttpActionResult RefundSale([FromUri] int saleId)
        {
            return PrepareResponse(FinanceProcesses.RefundSale(saleId, _context));
        }

        [HttpPost]
        [Route("creditStudent")]
        [Authorize(Roles = "Finance")]
        public IHttpActionResult CreditStudentAccount([FromBody] FinanceTransaction transaction)
        {
            return PrepareResponse(FinanceProcesses.ProcessManualTransaction(transaction, _context, true));
        }

        [HttpPost]
        [Route("debitStudent")]
        [Authorize(Roles = "Staff, SeniorStaff")]
        public IHttpActionResult DebitStudentAccount([FromBody] FinanceTransaction transaction)
        {
            return PrepareResponse(FinanceProcesses.ProcessManualTransaction(transaction, _context));
        }

        [HttpGet]
        [Route("getStudentBalance/{studentId:int}")]
        public decimal GetBalance([FromUri] int studentId)
        {
            AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(FinanceProcesses.GetStudentBalance(studentId, _context));
        }
    }
}
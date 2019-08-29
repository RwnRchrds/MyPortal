using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
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
        [HttpPost]
        [Route("basketItems/create")]
        public IHttpActionResult CreateBasketItem([FromBody] FinanceBasketItem basketItem)
        {
            return PrepareResponse(FinanceProcesses.CreateBasketItem(basketItem, _context));
        }

        [HttpGet]
        [Route("basket/{studentId:int}")]
        public IEnumerable<FinanceBasketItemDto> GetBasketItemsForStudent([FromUri] int studentId)
        {
            return PrepareResponseObject(FinanceProcesses.GetBasketItemsForStudent(studentId, _context));
        }

        [HttpGet]
        [Route("basket/total/{studentId:int}")]
        public decimal GetTotal([FromUri] int studentId)
        {
            return PrepareResponseObject(FinanceProcesses.GetBasketTotalForStudent(studentId, _context));
        }

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

        [HttpGet]
        [Route("products/get/available/{studentId:int}")]
        public IEnumerable<FinanceProductDto> GetAvailableProductsForStudent([FromUri] int studentId)
        {
            return PrepareResponseObject(FinanceProcesses.GetAvailableProductsForStudent(studentId, _context));
        }
 
        [HttpGet]
        [Route("products/price/{productId:int}")]
        public decimal GetProductPrice([FromUri] int productId)
        {
            return PrepareResponseObject(FinanceProcesses.GetProductPrice(productId, _context));
        }

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

        [HttpPost]
        [Route("products/create")]
        public IHttpActionResult NewProduct([FromBody] FinanceProduct product)
        {
            return PrepareResponse(FinanceProcesses.CreateProduct(product, _context));
        }

        [HttpPost]
        [Route("products/update")]
        public IHttpActionResult UpdateProduct([FromBody] FinanceProduct product)
        {
            return PrepareResponse(FinanceProcesses.UpdateProduct(product, _context));
        }

        [HttpPost]
        [Route("sales/queryBalance")]
        public bool AssessBalance([FromBody] FinanceSale sale)
        {
            return PrepareResponseObject(FinanceProcesses.AssessBalance(sale, _context));
        }

        [HttpDelete]
        [Route("sales/delete/{saleId:int}")]
        public IHttpActionResult DeleteSale([FromUri] int saleId)
        {
            return PrepareResponse(FinanceProcesses.DeleteSale(saleId, _context));
        }

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

        [HttpGet]
        [Route("sales/get/byStudent/{studentId:int}")]
        public IEnumerable<FinanceSaleDto> GetSalesForStudent([FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponseObject(FinanceProcesses.GetAllSalesForStudent(studentId, academicYearId, _context));
        }

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
 
        [HttpPost]
        [Route("sales/create")]
        public IHttpActionResult CreateSale([FromBody] FinanceSale sale)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponse(FinanceProcesses.CreateSale(sale, academicYearId, _context));
        }

        [HttpPost]
        [Route("sales/checkoutBasket/{studentId:int}")]
        public IHttpActionResult Purchase([FromBody] int studentId)
        {
            AuthenticateStudentRequest(studentId);
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponse(FinanceProcesses.CheckoutBasketForStudent(studentId, academicYearId, _context));
        }
 
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
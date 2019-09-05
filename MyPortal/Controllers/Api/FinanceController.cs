using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web.Http;
using MyPortal.Dtos;
using MyPortal.Models.Attributes;
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
        [RequiresPermission("AccessStudentStore")]
        [Route("basketItems/create", Name = "ApiFinanceCreateBasketItem")]
        public IHttpActionResult CreateBasketItem([FromBody] FinanceBasketItem basketItem)
        {
            return PrepareResponse(FinanceProcesses.CreateBasketItem(basketItem, _context));
        }

        [RequiresPermission("AccessStudentStore")]
        [HttpGet]
        [Route("basketItems/get/byStudent/{studentId:int}", Name = "ApiFinanceGetBasketItemsByStudent")]
        public IEnumerable<FinanceBasketItemDto> GetBasketItemsByStudent([FromUri] int studentId)
        {
            return PrepareResponseObject(FinanceProcesses.GetBasketItemsByStudent(studentId, _context));
        }

        [RequiresPermission("AccessStudentStore")]
        [HttpGet]
        [Route("basket/total/{studentId:int}", Name = "ApiFinanceGetBasketTotalForStudent")]
        public decimal GetBasketTotalForStudent([FromUri] int studentId)
        {
            return PrepareResponseObject(FinanceProcesses.GetBasketTotalForStudent(studentId, _context));
        }

        [HttpDelete]
        [RequiresPermission("AccessStudentStore")]
        [Route("basketItems/delete/{basketItemId:int}", Name = "ApiFinanceDeleteBasketItem")]
        public IHttpActionResult RemoveFromBasket([FromUri] int basketItemId)
        {
            return PrepareResponse(FinanceProcesses.DeleteBasketItem(basketItemId, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditProducts")]
        [Route("products/delete/{productId:int}", Name = "ApiFinanceDeleteProduct")]
        public IHttpActionResult DeleteProduct([FromUri] int productId)
        {
            return PrepareResponse(FinanceProcesses.DeleteProduct(productId, _context));
        }

        [HttpGet]
        [RequiresPermission("AccessStudentStore")]
        [Route("products/get/available/{studentId:int}", Name = "ApiFinanceGetAvailableProductsByStudent")]
        public IEnumerable<FinanceProductDto> GetAvailableProductsByStudent([FromUri] int studentId)
        {
            return PrepareResponseObject(FinanceProcesses.GetAvailableProductsByStudent(studentId, _context));
        }
 
        [HttpGet]
        [RequiresPermission("ViewProducts, AccessStudentStore")]
        [Route("products/price/{productId:int}", Name = "ApiFinanceGetProductPrice")]
        public decimal GetProductPrice([FromUri] int productId)
        {
            return PrepareResponseObject(FinanceProcesses.GetProductPrice(productId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewProducts, AccessStudentStore")]
        [Route("products/get/byId/{productId:int}", Name = "ApiFinanceGetProductById")]
        public FinanceProductDto GetProductById([FromUri] int productId)
        {
            return PrepareResponseObject(FinanceProcesses.GetProductById(productId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewProducts")]
        [Route("products/get/all", Name = "ApiFinanceGetAllProducts")]
        public IEnumerable<FinanceProductDto> GetAllProducts()
        {
            return PrepareResponseObject(FinanceProcesses.GetAllProducts(_context));
        }

        [HttpPost]
        [RequiresPermission("ViewProducts")]
        [Route("products/get/dataGrid/all", Name = "ApiFinanceGetAllProductsDataGrid")]
        public IHttpActionResult GetAllProductsDataGrid([FromBody] DataManagerRequest dm)
        {
            var products = PrepareResponseObject(FinanceProcesses.GetAllProducts_DataGrid(_context));

            return PrepareDataGridObject(products, dm);
        }

        [HttpPost]
        [RequiresPermission("EditProducts")]
        [Route("products/create", Name = "ApiFinanceCreateProduct")]
        public IHttpActionResult CreateProduct([FromBody] FinanceProduct product)
        {
            return PrepareResponse(FinanceProcesses.CreateProduct(product, _context));
        }

        [HttpPost]
        [RequiresPermission("EditProducts")]
        [Route("products/update", Name = "ApiFinanceUpdateProduct")]
        public IHttpActionResult UpdateProduct([FromBody] FinanceProduct product)
        {
            return PrepareResponse(FinanceProcesses.UpdateProduct(product, _context));
        }

        [HttpPost]
        [RequiresPermission("EditSales")]
        [Route("sales/queryBalance", Name = "ApiFinanceAssessBalance")]
        public bool AssessBalance([FromBody] FinanceSale sale)
        {
            return PrepareResponseObject(FinanceProcesses.AssessBalance(sale, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditSales")]
        [Route("sales/delete/{saleId:int}", Name = "ApiFinanceDeleteSale")]
        public IHttpActionResult DeleteSale([FromUri] int saleId)
        {
            return PrepareResponse(FinanceProcesses.DeleteSale(saleId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/processed", Name = "ApiFinanceGetProcessedSales")]
        public IEnumerable<FinanceSaleDto> GetProcessedSales()
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponseObject(FinanceProcesses.GetProcessedSales(academicYearId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/dataGrid/processed", Name = "ApiFinanceGetProcessedSalesDataGrid")]
        public IHttpActionResult GetProcessedSalesDataGrid([FromBody] DataManagerRequest dm)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var sales = PrepareResponseObject(FinanceProcesses.GetProcessedSales_DataGrid(academicYearId, _context));

            return PrepareDataGridObject(sales, dm);
        }
 
        [HttpGet]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/all", Name = "ApiFinanceGetAllSales")]
        public IEnumerable<FinanceSaleDto> GetAllSales()
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponseObject(FinanceProcesses.GetAllSales(academicYearId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/dataGrid/all", Name = "ApiFinanceGetAllSalesDataGrid")]
        public IHttpActionResult GetAllSalesDataGrid([FromBody] DataManagerRequest dm)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var sales = PrepareResponseObject(FinanceProcesses.GetAllSales_DataGrid(academicYearId, _context));

            return PrepareDataGridObject(sales, dm);
        }

        [HttpGet]
        [RequiresPermission("ViewSales, AccessStudentPortal")]
        [Route("sales/get/byStudent/{studentId:int}", Name = "ApiFinanceGetSalesByStudent")]
        public IEnumerable<FinanceSaleDto> GetSalesByStudent([FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponseObject(FinanceProcesses.GetAllSalesByStudent(studentId, academicYearId, _context));
        }

        [HttpGet]
        [Route("sales/get/pending", Name = "ApiFinanceGetPendingSales")]
        [RequiresPermission("ViewSales")]
        public IEnumerable<FinanceSaleDto> GetPendingSales()
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponseObject(FinanceProcesses.GetPendingSales(academicYearId, _context));
        }

        [HttpPost]
        [Route("sales/get/dataGrid/pending", Name = "ApiFinanceGetPendingSalesDataGrid")]
        [RequiresPermission("ViewSales")]
        public IHttpActionResult GetPendingSalesDataGrid([FromBody] DataManagerRequest dm)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var sales = PrepareResponseObject(FinanceProcesses.GetPendingSales_DataGrid(academicYearId, _context));

            return PrepareDataGridObject(sales, dm);
        }
 
        [HttpPost]
        [RequiresPermission("EditSales")]
        [Route("sales/markComplete/{saleId:int}", Name = "ApiFinanceMarkSaleProcessed")]
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
        [RequiresPermission("EditSales")]
        [Route("sales/create", Name = "ApiFinanceCreateSale")]
        public IHttpActionResult CreateSale([FromBody] FinanceSale sale)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponse(FinanceProcesses.CreateSale(sale, academicYearId, _context));
        }

        [HttpPost]
        [RequiresPermission("AccessStudentStore")]
        [Route("sales/checkoutBasket/{studentId:int}", Name = "ApiFinanceCheckoutBasket")]
        public IHttpActionResult CheckoutBasket([FromBody] int studentId)
        {
            AuthenticateStudentRequest(studentId);
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponse(FinanceProcesses.CheckoutBasketForStudent(studentId, academicYearId, _context));
        }
 
        [HttpPost]
        [RequiresPermission("EditSales")]
        [Route("sales/refund/{saleId:int}", Name = "ApiFinanceRefundSale")]
        public IHttpActionResult RefundSale([FromUri] int saleId)
        {
            return PrepareResponse(FinanceProcesses.RefundSale(saleId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditAccounts")]
        [Route("creditStudent", Name = "ApiFinanceCreditStudent")]
        public IHttpActionResult CreditStudentAccount([FromBody] FinanceTransaction transaction)
        {
            return PrepareResponse(FinanceProcesses.ProcessManualTransaction(transaction, _context, true));
        }

        [HttpPost]
        [Route("debitStudent", Name = "ApiFinanceDebitStudent")]
        [RequiresPermission("EditAccounts")]
        public IHttpActionResult DebitStudentAccount([FromBody] FinanceTransaction transaction)
        {
            return PrepareResponse(FinanceProcesses.ProcessManualTransaction(transaction, _context));
        }

        [HttpGet]
        [Route("getStudentBalance/{studentId:int}", Name = "ApiFinanceGetStudentBalance")]
        public decimal GetBalance([FromUri] int studentId)
        {
            AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(FinanceProcesses.GetStudentBalance(studentId, _context));
        }
    }
}
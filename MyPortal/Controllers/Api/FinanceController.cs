using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> CreateBasketItem([FromBody] FinanceBasketItem basketItem)
        {
            try
            {
                await FinanceProcesses.CreateBasketItem(basketItem, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Item added to basket");
        }

        [RequiresPermission("AccessStudentStore")]
        [HttpGet]
        [Route("basketItems/get/byStudent/{studentId:int}", Name = "ApiFinanceGetBasketItemsByStudent")]
        public async Task<IEnumerable<FinanceBasketItemDto>> GetBasketItemsByStudent([FromUri] int studentId)
        {
            try
            {
                return await FinanceProcesses.GetBasketItemsByStudent(studentId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [RequiresPermission("AccessStudentStore")]
        [HttpGet]
        [Route("basket/total/{studentId:int}", Name = "ApiFinanceGetBasketTotalForStudent")]
        public async Task<decimal> GetBasketTotalForStudent([FromUri] int studentId)
        {
            try
            {
                return await FinanceProcesses.GetBasketTotalForStudent(studentId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("AccessStudentStore")]
        [Route("basketItems/delete/{basketItemId:int}", Name = "ApiFinanceDeleteBasketItem")]
        public async Task<IHttpActionResult> RemoveFromBasket([FromUri] int basketItemId)
        {
            try
            {
                await FinanceProcesses.DeleteBasketItem(basketItemId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Item removed from basket");
        }

        [HttpDelete]
        [RequiresPermission("EditProducts")]
        [Route("products/delete/{productId:int}", Name = "ApiFinanceDeleteProduct")]
        public async Task<IHttpActionResult> DeleteProduct([FromUri] int productId)
        {
            try
            {
                await FinanceProcesses.DeleteProduct(productId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Product deleted");
        }

        [HttpGet]
        [RequiresPermission("AccessStudentStore")]
        [Route("products/get/available/{studentId:int}", Name = "ApiFinanceGetAvailableProductsByStudent")]
        public async Task<IEnumerable<FinanceProductDto>> GetAvailableProductsByStudent([FromUri] int studentId)
        {
            try
            {
                return await FinanceProcesses.GetAvailableProductsByStudent(studentId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
 
        [HttpGet]
        [RequiresPermission("ViewProducts, AccessStudentStore")]
        [Route("products/price/{productId:int}", Name = "ApiFinanceGetProductPrice")]
        public async Task<decimal> GetProductPrice([FromUri] int productId)
        {
            try
            {
                return await FinanceProcesses.GetProductPrice(productId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewProducts, AccessStudentStore")]
        [Route("products/get/byId/{productId:int}", Name = "ApiFinanceGetProductById")]
        public async Task<FinanceProductDto> GetProductById([FromUri] int productId)
        {
            try
            {
                return await FinanceProcesses.GetProductById(productId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewProducts")]
        [Route("products/get/all", Name = "ApiFinanceGetAllProducts")]
        public async Task<IEnumerable<FinanceProductDto>> GetAllProducts()
        {
            try
            {
                return await FinanceProcesses.GetAllProducts(_context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewProducts")]
        [Route("products/get/dataGrid/all", Name = "ApiFinanceGetAllProductsDataGrid")]
        public async Task<IHttpActionResult> GetAllProductsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var products = await FinanceProcesses.GetAllProductsDataGrid(_context);
                return PrepareDataGridObject(products, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditProducts")]
        [Route("products/create", Name = "ApiFinanceCreateProduct")]
        public async Task<IHttpActionResult> CreateProduct([FromBody] FinanceProduct product)
        {
            try
            {
                await FinanceProcesses.CreateProduct(product, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Product created");
        }

        [HttpPost]
        [RequiresPermission("EditProducts")]
        [Route("products/update", Name = "ApiFinanceUpdateProduct")]
        public async Task<IHttpActionResult> UpdateProduct([FromBody] FinanceProduct product)
        {
            try
            {
                await FinanceProcesses.UpdateProduct(product, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Product updated");
        }

        [HttpPost]
        [RequiresPermission("EditSales")]
        [Route("sales/queryBalance", Name = "ApiFinanceAssessBalance")]
        public async Task<bool> AssessBalance([FromBody] FinanceSale sale)
        {
            try
            {
                return await FinanceProcesses.AssessBalance(sale, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditSales")]
        [Route("sales/delete/{saleId:int}", Name = "ApiFinanceDeleteSale")]
        public async Task<IHttpActionResult> DeleteSale([FromUri] int saleId)
        {
            try
            {
                await FinanceProcesses.DeleteSale(saleId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Sale deleted");
        }

        [HttpGet]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/processed", Name = "ApiFinanceGetProcessedSales")]
        public async Task<IEnumerable<FinanceSaleDto>> GetProcessedSales()
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponseObject(FinanceProcesses.GetProcessedSales(academicYearId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/dataGrid/processed", Name = "ApiFinanceGetProcessedSalesDataGrid")]
        public async Task<IHttpActionResult> GetProcessedSalesDataGrid([FromBody] DataManagerRequest dm)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var sales = PrepareResponseObject(FinanceProcesses.GetProcessedSalesDataGrid(academicYearId, _context));

            return PrepareDataGridObject(sales, dm);
        }
 
        [HttpGet]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/all", Name = "ApiFinanceGetAllSales")]
        public async Task<IEnumerable<FinanceSaleDto>> GetAllSales()
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponseObject(FinanceProcesses.GetAllSales(academicYearId, _context));
        }

        [HttpPost]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/dataGrid/all", Name = "ApiFinanceGetAllSalesDataGrid")]
        public async Task<IHttpActionResult> GetAllSalesDataGrid([FromBody] DataManagerRequest dm)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var sales = PrepareResponseObject(FinanceProcesses.GetAllSalesDataGrid(academicYearId, _context));

            return PrepareDataGridObject(sales, dm);
        }

        [HttpGet]
        [RequiresPermission("ViewSales, AccessStudentPortal")]
        [Route("sales/get/byStudent/{studentId:int}", Name = "ApiFinanceGetSalesByStudent")]
        public async Task<IEnumerable<FinanceSaleDto>> GetSalesByStudent([FromUri] int studentId)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponseObject(FinanceProcesses.GetAllSalesByStudent(studentId, academicYearId, _context));
        }

        [HttpGet]
        [Route("sales/get/pending", Name = "ApiFinanceGetPendingSales")]
        [RequiresPermission("ViewSales")]
        public async Task<IEnumerable<FinanceSaleDto>> GetPendingSales()
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponseObject(FinanceProcesses.GetPendingSales(academicYearId, _context));
        }

        [HttpPost]
        [Route("sales/get/dataGrid/pending", Name = "ApiFinanceGetPendingSalesDataGrid")]
        [RequiresPermission("ViewSales")]
        public async Task<IHttpActionResult> GetPendingSalesDataGrid([FromBody] DataManagerRequest dm)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            var sales = PrepareResponseObject(FinanceProcesses.GetPendingSalesDataGrid(academicYearId, _context));

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
        public async Task<IHttpActionResult> CreateSale([FromBody] FinanceSale sale)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return PrepareResponse(FinanceProcesses.CreateSale(sale, academicYearId, _context));
        }

        [HttpPost]
        [RequiresPermission("AccessStudentStore")]
        [Route("sales/checkoutBasket/{studentId:int}", Name = "ApiFinanceCheckoutBasket")]
        public async Task<IHttpActionResult> CheckoutBasket([FromBody] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

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
            return PrepareResponse(FinanceProcesses.ProcessManualTransaction(transaction, _context));
        }

        [HttpPost]
        [Route("debitStudent", Name = "ApiFinanceDebitStudent")]
        [RequiresPermission("EditAccounts")]
        public IHttpActionResult DebitStudentAccount([FromBody] FinanceTransaction transaction)
        {
            return PrepareResponse(FinanceProcesses.ProcessManualTransaction(transaction, _context, true));
        }

        [HttpGet]
        [Route("getStudentBalance/{studentId:int}", Name = "ApiFinanceGetStudentBalance")]
        public async Task<decimal> GetBalance([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            return PrepareResponseObject(FinanceProcesses.GetStudentBalance(studentId, _context));
        }
    }
}
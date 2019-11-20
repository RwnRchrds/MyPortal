using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.DynamicData;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Dtos.DataGrid;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/finance")]
    public class FinanceController : MyPortalApiController
    {
        private readonly FinanceService _service;

        public FinanceController()
        {
            _service = new FinanceService();
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }

        public FinanceController(IUnitOfWork unitOfWork) : base (unitOfWork)
        {
            _service = new FinanceService(UnitOfWork);
        }

        [HttpPost]
        [RequiresPermission("AccessStudentStore")]
        [Route("basketItems/create", Name = "ApiCreateBasketItem")]
        public async Task<IHttpActionResult> CreateBasketItem([FromBody] FinanceBasketItem basketItem)
        {
            try
            {
                await _service.CreateBasketItem(basketItem);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Item added to basket");
        }

        [RequiresPermission("AccessStudentStore")]
        [HttpGet]
        [Route("basketItems/get/byStudent/{studentId:int}", Name = "ApiGetBasketItemsByStudent")]
        public async Task<IEnumerable<FinanceBasketItemDto>> GetBasketItemsByStudent([FromUri] int studentId)
        {
            try
            {
                var basketItems = await _service.GetBasketItemsByStudent(studentId);

                return basketItems.Select(Mapper.Map<FinanceBasketItem, FinanceBasketItemDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [RequiresPermission("AccessStudentStore")]
        [HttpGet]
        [Route("basket/total/{studentId:int}", Name = "ApiGetBasketTotalForStudent")]
        public async Task<decimal> GetBasketTotalForStudent([FromUri] int studentId)
        {
            try
            {
                var total = await _service.GetBasketTotalForStudent(studentId);

                return total;
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("AccessStudentStore")]
        [Route("basketItems/delete/{basketItemId:int}", Name = "ApiDeleteBasketItem")]
        public async Task<IHttpActionResult> RemoveFromBasket([FromUri] int basketItemId)
        {
            try
            {
                await _service.DeleteBasketItem(basketItemId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Item removed from basket");
        }

        [HttpDelete]
        [RequiresPermission("EditProducts")]
        [Route("products/delete/{productId:int}", Name = "ApiDeleteProduct")]
        public async Task<IHttpActionResult> DeleteProduct([FromUri] int productId)
        {
            try
            {
                await _service.DeleteProduct(productId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Product deleted");
        }

        [HttpGet]
        [RequiresPermission("AccessStudentStore")]
        [Route("products/get/available/{studentId:int}", Name = "ApiGetAvailableProductsByStudent")]
        public async Task<IEnumerable<FinanceProductDto>> GetAvailableProductsByStudent([FromUri] int studentId)
        {
            try
            {
                var products = await _service.GetAvailableProductsByStudent(studentId);

                return products.Select(Mapper.Map<FinanceProduct, FinanceProductDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
 
        [HttpGet]
        [RequiresPermission("ViewProducts, AccessStudentStore")]
        [Route("products/price/{productId:int}", Name = "ApiGetProductPrice")]
        public async Task<decimal> GetProductPrice([FromUri] int productId)
        {
            try
            {
                var price = await _service.GetProductPrice(productId);

                return price;
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewProducts, AccessStudentStore")]
        [Route("products/get/byId/{productId:int}", Name = "ApiGetProductById")]
        public async Task<FinanceProductDto> GetProductById([FromUri] int productId)
        {
            try
            {
                var product = await _service.GetProductById(productId);

                return Mapper.Map<FinanceProduct, FinanceProductDto>(product);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewProducts")]
        [Route("products/get/all", Name = "ApiGetAllProducts")]
        public async Task<IEnumerable<FinanceProductDto>> GetAllProducts()
        {
            try
            {
                var products = await _service.GetAllProducts();

                return products.Select(Mapper.Map<FinanceProduct, FinanceProductDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewProducts")]
        [Route("products/get/dataGrid/all", Name = "ApiGetAllProductsDataGrid")]
        public async Task<IHttpActionResult> GetAllProductsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var products = await _service.GetAllProducts();

                var list = products.Select(Mapper.Map<FinanceProduct, GridFinanceProductDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditProducts")]
        [Route("products/create", Name = "ApiCreateProduct")]
        public async Task<IHttpActionResult> CreateProduct([FromBody] FinanceProduct product)
        {
            try
            {
                await _service.CreateProduct(product);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Product created");
        }

        [HttpPost]
        [RequiresPermission("EditProducts")]
        [Route("products/update", Name = "ApiUpdateProduct")]
        public async Task<IHttpActionResult> UpdateProduct([FromBody] FinanceProduct product)
        {
            try
            {
                await _service.UpdateProduct(product);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Product updated");
        }

        [HttpPost]
        [RequiresPermission("EditSales")]
        [Route("sales/queryBalance", Name = "ApiAssessBalance")]
        public async Task<bool> AssessBalance([FromBody] FinanceSale sale)
        {
            try
            {
                var check = await _service.AssessBalance(sale);

                return check;
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditSales")]
        [Route("sales/delete/{saleId:int}", Name = "ApiDeleteSale")]
        public async Task<IHttpActionResult> DeleteSale([FromUri] int saleId)
        {
            try
            {
                await _service.DeleteSale(saleId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Sale deleted");
        }

        [HttpGet]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/processed", Name = "ApiGetProcessedSales")]
        public async Task<IEnumerable<FinanceSaleDto>> GetProcessedSales()
        {
            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);

                    var sales = await _service.GetProcessedSales(academicYearId);

                    return sales.Select(Mapper.Map<FinanceSale, FinanceSaleDto>);
                }
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/dataGrid/processed", Name = "ApiGetProcessedSalesDataGrid")]
        public async Task<IHttpActionResult> GetProcessedSalesDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);

                    var sales = await _service.GetProcessedSales(academicYearId);

                    var list = sales.Select(Mapper.Map<FinanceSale, GridFinanceSaleDto>);

                    return PrepareDataGridObject(list, dm);
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
 
        [HttpGet]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/all", Name = "ApiGetAllSales")]
        public async Task<IEnumerable<FinanceSaleDto>> GetAllSales()
        {
            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);

                    var sales = await _service.GetAllSales(academicYearId);

                    return sales.Select(Mapper.Map<FinanceSale, FinanceSaleDto>);
                }
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/dataGrid/all", Name = "ApiGetAllSalesDataGrid")]
        public async Task<IHttpActionResult> GetAllSalesDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);

                    var sales = await _service.GetAllSales(academicYearId);

                    var list = sales.Select(Mapper.Map<FinanceSale, GridFinanceSaleDto>);

                    return PrepareDataGridObject(list, dm);
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewSales, AccessStudentPortal")]
        [Route("sales/get/byStudent/{studentId:int}", Name = "ApiGetSalesByStudent")]
        public async Task<IEnumerable<FinanceSaleDto>> GetSalesByStudent([FromUri] int studentId)
        {
            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);

                    var sales = await _service.GetAllSalesByStudent(studentId, academicYearId);

                    return sales.Select(Mapper.Map<FinanceSale, FinanceSaleDto>);
                }
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [Route("sales/get/pending", Name = "ApiGetPendingSales")]
        [RequiresPermission("ViewSales")]
        public async Task<IEnumerable<FinanceSaleDto>> GetPendingSales()
        {
            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);

                    var sales = await _service.GetPendingSales(academicYearId);

                    return sales.Select(Mapper.Map<FinanceSale, FinanceSaleDto>);
                }
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [Route("sales/get/dataGrid/pending", Name = "ApiGetPendingSalesDataGrid")]
        [RequiresPermission("ViewSales")]
        public async Task<IHttpActionResult> GetPendingSalesDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);

                    var sales = await _service.GetPendingSales(academicYearId);

                    var list = sales.Select(Mapper.Map<FinanceSale, GridFinanceSaleDto>);

                    return PrepareDataGridObject(list, dm);
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
 
        [HttpPost]
        [RequiresPermission("EditSales")]
        [Route("sales/markComplete/{saleId:int}", Name = "ApiMarkSaleProcessed")]
        public async Task<IHttpActionResult> MarkSaleProcessed([FromUri] int saleId)
        {
            try
            {
                await _service.MarkSaleProcessed(saleId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Sale marked as processed");
        }
 
        [HttpPost]
        [RequiresPermission("EditSales")]
        [Route("sales/create", Name = "ApiCreateSale")]
        public async Task<IHttpActionResult> CreateSale([FromBody] FinanceSale sale)
        {
            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);

                    await _service.CreateSale(sale, academicYearId);
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Sale created");
        }

        [HttpPost]
        [RequiresPermission("AccessStudentStore")]
        [Route("sales/checkoutBasket/{studentId:int}", Name = "ApiCheckoutBasket")]
        public async Task<IHttpActionResult> CheckoutBasket([FromBody] int studentId)
        {
            await AuthenticateStudentRequest(studentId);

            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);
                    
                    await _service.CheckoutBasketForStudent(studentId, academicYearId);
                }
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Sale completed");
        }
 
        [HttpPost]
        [RequiresPermission("EditSales")]
        [Route("sales/refund/{saleId:int}", Name = "ApiRefundSale")]
        public async Task<IHttpActionResult> RefundSale([FromUri] int saleId)
        {
            try
            {
                await _service.RefundSale(saleId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Sale refunded");
        }

        [HttpPost]
        [RequiresPermission("EditAccounts")]
        [Route("creditStudent", Name = "ApiCreditStudent")]
        public async Task<IHttpActionResult> CreditStudentAccount([FromBody] FinanceTransaction transaction)
        {
            try
            {
                await _service.ProcessManualTransaction(transaction);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Account credited");
        }

        [HttpPost]
        [Route("debitStudent", Name = "ApiDebitStudent")]
        [RequiresPermission("EditAccounts")]
        public async Task<IHttpActionResult> DebitStudentAccount([FromBody] FinanceTransaction transaction)
        {
            try
            {
                await _service.ProcessManualTransaction(transaction, true);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Account debited");
        }

        [HttpGet]
        [Route("getStudentBalance/{studentId:int}", Name = "ApiGetStudentBalance")]
        public async Task<decimal> GetBalance([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            try
            {
                var balance = await _service.GetStudentBalance(studentId);

                return balance;
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
    }
}
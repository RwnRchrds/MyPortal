using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using MyPortal.Attributes.Filters;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Dtos.DataGrid;
using MyPortal.BusinessLogic.Models.Data;
using MyPortal.BusinessLogic.Services;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/finance")]
    [ValidateModel]
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

        [HttpPost]
        [Route("basketItems/create", Name = "ApiCreateBasketItem")]
        public async Task<IHttpActionResult> CreateBasketItem([FromBody] BasketItemDto basketItem)
        {
            try
            {
                await _service.CreateBasketItem(basketItem);
                
                return Ok("Item added to basket");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("basketItems/get/byStudent/{studentId:int}", Name = "ApiGetBasketItemsByStudent")]
        public async Task<IHttpActionResult> GetBasketItemsByStudent([FromUri] int studentId)
        {
            try
            {
                var basketItems = await _service.GetBasketItemsByStudent(studentId);

                return Ok(basketItems);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("basket/total/{studentId:int}", Name = "ApiGetBasketTotalForStudent")]
        public async Task<IHttpActionResult> GetBasketTotalForStudent([FromUri] int studentId)
        {
            try
            {
                var total = await _service.GetBasketTotalForStudent(studentId);

                return Ok(total);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("basketItems/delete/{basketItemId:int}", Name = "ApiDeleteBasketItem")]
        public async Task<IHttpActionResult> RemoveFromBasket([FromUri] int basketItemId)
        {
            try
            {
                await _service.DeleteBasketItem(basketItemId);

                return Ok("Item removed from basket");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditProducts")]
        [Route("products/delete/{productId:int}", Name = "ApiDeleteProduct")]
        public async Task<IHttpActionResult> DeleteProduct([FromUri] int productId)
        {
            try
            {
                await _service.DeleteProduct(productId);

                return Ok("Product deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("products/get/available/{studentId:int}", Name = "ApiGetAvailableProductsByStudent")]
        public async Task<IHttpActionResult> GetAvailableProductsByStudent([FromUri] int studentId)
        {
            try
            {
                var products = (await _service.GetAvailableProductsByStudent(studentId)).Select(_mapping.Map<ProductDto>);

                return Ok(products);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
 
        [HttpGet]
        [RequiresPermission("ViewProducts")]
        [Route("products/price/{productId:int}", Name = "ApiGetProductPrice")]
        public async Task<IHttpActionResult> GetProductPrice([FromUri] int productId)
        {
            try
            {
                var price = await _service.GetProductPrice(productId);

                return Ok(price);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewProducts")]
        [Route("products/get/byId/{productId:int}", Name = "ApiGetProductById")]
        public async Task<IHttpActionResult> GetProductById([FromUri] int productId)
        {
            try
            {
                var product = await _service.GetProductById(productId);

                return Ok(product);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewProducts")]
        [Route("products/get/all", Name = "ApiGetAllProducts")]
        public async Task<IHttpActionResult> GetAllProducts()
        {
            try
            {
                var products = (await _service.GetAllProducts()).Select(_mapping.Map<ProductDto>);

                return Ok(products);
            }
            catch (Exception e)
            {
                return HandleException(e);
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

                var list = products.Select(_mapping.Map<DataGridProductDto>);

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
        public async Task<IHttpActionResult> CreateProduct([FromBody] ProductDto product)
        {
            try
            {
                await _service.CreateProduct(product);

                return Ok("Product created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditProducts")]
        [Route("products/update", Name = "ApiUpdateProduct")]
        public async Task<IHttpActionResult> UpdateProduct([FromBody] ProductDto product)
        {
            try
            {
                await _service.UpdateProduct(product);

                return Ok("Product updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditSales")]
        [Route("sales/queryBalance", Name = "ApiAssessBalance")]
        public async Task<IHttpActionResult> AssessBalance([FromBody] SaleDto sale)
        {
            try
            {
                var check = await _service.AssessBalance(sale);

                return Ok(check);
            }
            catch (Exception e)
            {
                return HandleException(e);
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

                return Ok("Sale deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/processed", Name = "ApiGetProcessedSales")]
        public async Task<IHttpActionResult> GetProcessedSales()
        {
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                var sales = await _service.GetProcessedSales(academicYearId);

                return Ok(sales);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/dataGrid/processed", Name = "ApiGetProcessedSalesDataGrid")]
        public async Task<IHttpActionResult> GetProcessedSalesDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                var sales = await _service.GetProcessedSales(academicYearId);

                var list = sales.Select(_mapping.Map<DataGridSaleDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
 
        [HttpGet]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/all", Name = "ApiGetAllSales")]
        public async Task<IHttpActionResult> GetAllSales()
        {
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                var sales = await _service.GetAllSales(academicYearId);

                return Ok(sales);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/dataGrid/all", Name = "ApiGetAllSalesDataGrid")]
        public async Task<IHttpActionResult> GetAllSalesDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                var sales = await _service.GetAllSales(academicYearId);

                var list = sales.Select(_mapping.Map<DataGridSaleDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewSales")]
        [Route("sales/get/byStudent/{studentId:int}", Name = "ApiGetSalesByStudent")]
        public async Task<IHttpActionResult> GetSalesByStudent([FromUri] int studentId)
        {
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                var sales = await _service.GetAllSalesByStudent(studentId, academicYearId);

                return Ok(sales);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("sales/get/pending", Name = "ApiGetPendingSales")]
        [RequiresPermission("ViewSales")]
        public async Task<IHttpActionResult> GetPendingSales()
        {
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                var sales = await _service.GetPendingSales(academicYearId);

                return Ok(sales);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("sales/get/dataGrid/pending", Name = "ApiGetPendingSalesDataGrid")]
        [RequiresPermission("ViewSales")]
        public async Task<IHttpActionResult> GetPendingSalesDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                var sales = await _service.GetPendingSales(academicYearId);

                var list = sales.Select(_mapping.Map<DataGridSaleDto>);

                return PrepareDataGridObject(list, dm);
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
                
                return Ok("Sale marked as processed");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
 
        [HttpPost]
        [RequiresPermission("EditSales")]
        [Route("sales/create", Name = "ApiCreateSale")]
        public async Task<IHttpActionResult> CreateSale([FromBody] SaleDto sale)
        {
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                await _service.CreateSale(sale, academicYearId);

                return Ok("Sale created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("sales/checkoutBasket/{studentId:int}", Name = "ApiCheckoutBasket")]
        public async Task<IHttpActionResult> CheckoutBasket([FromBody] int studentId)
        {
            await AuthenticateStudentRequest(studentId);

            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                await _service.CheckoutBasketForStudent(studentId, academicYearId);

                return Ok("Sale completed");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
 
        [HttpPost]
        [RequiresPermission("EditSales")]
        [Route("sales/refund/{saleId:int}", Name = "ApiRefundSale")]
        public async Task<IHttpActionResult> RefundSale([FromUri] int saleId)
        {
            try
            {
                await _service.RefundSale(saleId);

                return Ok("Sale refunded");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditAccounts")]
        [Route("creditStudent", Name = "ApiCreditStudent")]
        public async Task<IHttpActionResult> CreditStudentAccount([FromBody] FinanceTransaction transaction)
        {
            try
            {
                await _service.ProcessManualTransaction(transaction);

                return Ok("Account credited");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("debitStudent", Name = "ApiDebitStudent")]
        [RequiresPermission("EditAccounts")]
        public async Task<IHttpActionResult> DebitStudentAccount([FromBody] FinanceTransaction transaction)
        {
            try
            {
                await _service.ProcessManualTransaction(transaction, true);
                
                return Ok("Account debited");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("getStudentBalance/{studentId:int}", Name = "ApiGetStudentBalance")]
        public async Task<IHttpActionResult> GetBalance([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            try
            {
                var balance = await _service.GetStudentBalance(studentId);

                return Ok(balance);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
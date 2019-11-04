using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Exceptions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using Syncfusion.EJ2.Charts;

namespace MyPortal.Services
{
    public class FinanceService : MyPortalService
    {
        public FinanceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        public async Task<bool> AssessBalance(FinanceSale sale)
        {
            using (var studentService = new StudentService(UnitOfWork))
            {
                var product = await GetProductById(sale.ProductId);

                var student = await studentService.GetStudentById(sale.StudentId);

                if (product == null)
                {
                    throw new ServiceException(ExceptionType.NotFound, "Product not found");
                }

                if (product.Type.IsMeal && student.FreeSchoolMeals)
                {
                    return true;
                }

                return student.AccountBalance >= product.Price;
            }
        }

        public async Task CheckoutBasketForStudent(int studentId, int academicYearId)
        {
            using (var studentService = new StudentService(UnitOfWork))
            {
                var student = await studentService.GetStudentById(studentId);

                var basket = student.FinanceBasketItems;

                if (!basket.Any())
                {
                    throw new ServiceException(ExceptionType.BadRequest, "There are no items in your basket");
                }

                if (basket.Sum(x => x.Product.Price) > student.AccountBalance)
                {
                    throw new ServiceException(ExceptionType.Forbidden, "Insufficient funds");
                }

                //Process sales for each item
                foreach (var item in basket)
                {
                    await CreateSale(new FinanceSale
                    {
                        AcademicYearId = academicYearId,
                        StudentId = studentId,
                        ProductId = item.ProductId
                    }, academicYearId, false);
                }

                UnitOfWork.FinanceBasketItems.RemoveRange(basket);

                await UnitOfWork.Complete();   
            }
        }

        public async Task CreateBasketItem(FinanceBasketItem basketItem)
        {
            using (var studentService = new StudentService(UnitOfWork))
            {
                var student = await studentService.GetStudentById(basketItem.StudentId);

                var product = await GetProductById(basketItem.ProductId);

                if (product == null)
                {
                    throw new ServiceException(ExceptionType.NotFound, "Product not found");
                }

                if (!product.Visible)
                {
                    throw new ServiceException(ExceptionType.Forbidden, "Product not available");
                }

                if ((student.FinanceBasketItems.Any(x => x.ProductId == basketItem.ProductId) ||
                     student.FinanceSales.Any(x => x.ProductId == basketItem.ProductId)) && product.OnceOnly)
                {
                    throw new ServiceException(ExceptionType.Forbidden, "This product cannot be purchased more than once");
                }

                UnitOfWork.FinanceBasketItems.Add(basketItem);
                await UnitOfWork.Complete();
            }
        }

        public async Task CreateProduct(FinanceProduct product)
        {
            if (!ValidationService.ModelIsValid(product))
            {
                throw new ServiceException(ExceptionType.BadRequest, "Invalid data");
            }

            UnitOfWork.FinanceProducts.Add(product);
            await UnitOfWork.Complete();
        }

        public async Task CreateSale(FinanceSale sale, int academicYearId, bool commitImmediately = true)
        {
            using (var studentService = new StudentService(UnitOfWork))
            {
                sale.Date = DateTime.Now;

                var student = await studentService.GetStudentById(sale.StudentId);

                var product = await GetProductById(sale.ProductId);

                if (product == null)
                {
                    throw new ServiceException(ExceptionType.NotFound, "Product not found");
                }

                if (product.Type.IsMeal)
                {
                    sale.Processed = true;
                }

                if (product.Type.IsMeal && student.FreeSchoolMeals)
                {
                    sale.AmountPaid = 0.00m;
                    sale.AcademicYearId = academicYearId;

                    UnitOfWork.FinanceSales.Add(sale);

                    if (commitImmediately)
                    {
                        await UnitOfWork.Complete();
                    }
                }

                else
                {
                    student.AccountBalance -= product.Price;

                    sale.AmountPaid = product.Price;
                    sale.AcademicYearId = academicYearId;

                    UnitOfWork.FinanceSales.Add(sale);

                    if (commitImmediately)
                    {
                        await UnitOfWork.Complete();
                    }
                }   
            }
        }

        public async Task DeleteBasketItem(int basketItemId)
        {
            var itemInDb = await GetBasketItemById(basketItemId);

            UnitOfWork.FinanceBasketItems.Remove(itemInDb);
            await UnitOfWork.Complete();
        }

        public async Task DeleteProduct(int productId)
        {
            var productInDb = await GetProductById(productId);

            productInDb.Deleted = true;
            //context.FinanceProducts.Remove(productInDb); //Delete from database
            await UnitOfWork.Complete();
        }

        public async Task DeleteSale(int saleId)
        {
            var saleInDb = await GetSaleById(saleId);

            saleInDb.Deleted = true;
            //context.FinanceSales.Remove(saleInDb); //Delete from database
            await UnitOfWork.Complete();
        }

        public async Task<IDictionary<int, string>> GetAllProductsLookup()
        {
            var products = await GetAllProducts();

            return products.ToDictionary(x => x.Id, x => x.Description);
        }

        public async Task<IEnumerable<FinanceProduct>> GetAllProducts()
        {
            return await UnitOfWork.FinanceProducts.GetAllAsync();
        }

        public async Task<IEnumerable<FinanceSale>> GetAllSales(int academicYearId)
        {
            return await UnitOfWork.FinanceSales.GetAllAsync(academicYearId);
        }

        public async Task<IEnumerable<FinanceSale>> GetAllSalesByStudent(int studentId,
            int academicYearId)
        {
            var sales = await UnitOfWork.FinanceSales.GetSalesByStudent(studentId, academicYearId);
            
            return sales;
        }

        public async Task<IEnumerable<FinanceProduct>> GetAvailableProductsByStudent(int studentId)
        {
            var items = await UnitOfWork.FinanceProducts.GetAvailableProductsByStudent(studentId);

            return items;
        }

        public async Task<FinanceBasketItem> GetBasketItemById(int basketItemId)
        {
            var item = await UnitOfWork.FinanceBasketItems.GetByIdAsync(basketItemId);

            if (item == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Item not found");
            }

            return item;
        }

        public async Task<IEnumerable<FinanceBasketItem>> GetBasketItemsByStudent(int studentId)
        {
            var basketItems = await UnitOfWork.FinanceBasketItems.GetBasketItemsByStudent(studentId);

            return basketItems;
        }

        public async Task<decimal> GetBasketTotalForStudent(int studentId)
        {
            var total = await UnitOfWork.FinanceBasketItems.GetBasketTotalForStudent(studentId);

            return total;
        }

        public async Task<IEnumerable<FinanceSale>> GetPendingSales(int academicYearId)
        {
            return await UnitOfWork.FinanceSales.GetPending(academicYearId);
        }

        public async Task<IEnumerable<FinanceSale>> GetProcessedSales(int academicYearId)
        {
            return await UnitOfWork.FinanceSales.GetProcessed(academicYearId);
        }

        public async Task<FinanceProduct> GetProductById(int productId)
        {
            var product = await UnitOfWork.FinanceProducts.GetByIdAsync(productId);

            if (product == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Product not found");
            }

            return product;
        }

        public async Task<FinanceSale> GetSaleById(int saleId)
        {
            var sale = await UnitOfWork.FinanceSales.GetByIdAsync(saleId);

            if (sale == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Sale not found");
            }

            return sale;
        }

        public async Task<decimal> GetProductPrice(int productId)
        {
            var productInDb = await GetProductById(productId);

            return productInDb.Price;
        }

        public async Task<IEnumerable<FinanceProductType>> GetAllProductTypes()
        {
            return await UnitOfWork.FinanceProductTypes.GetAllAsync();
        }

        public async Task<IDictionary<int, string>> GetAllProductTypesLookup()
        {
            var productTypes = await GetAllProductTypes();

            return productTypes.ToDictionary(x => x.Id, x => x.Description);
        }

        public async Task<decimal> GetStudentBalance(int studentId)
        {
            using (var studentService = new StudentService(UnitOfWork))
            {
                var student = await studentService.GetStudentById(studentId);

                return student.AccountBalance;
            }
        }

        public async Task ProcessManualTransaction(FinanceTransaction transaction, bool debit = false)
        {
            using (var studentService = new StudentService(UnitOfWork))
            {
                if (transaction.Amount <= 0)
                {
                    throw new ServiceException(ExceptionType.BadRequest, "Amount cannot be negative");
                }

                var studentInDb = await studentService.GetStudentById(transaction.StudentId);

                if (debit)
                {
                    transaction.Amount *= -1;
                }

                studentInDb.AccountBalance += transaction.Amount;

                await UnitOfWork.Complete();
            }
        }

        public async Task RefundSale(int saleId)
        {
            var saleInDb = await GetSaleById(saleId);

            saleInDb.Student.AccountBalance += saleInDb.AmountPaid;

            saleInDb.Processed = true;
            saleInDb.Refunded = true;
            await UnitOfWork.Complete();
        }

        public async Task UpdateProduct(FinanceProduct product)
        {
            var productInDb = await GetProductById(product.Id);

            productInDb.OnceOnly = product.OnceOnly;
            productInDb.Price = product.Price;
            productInDb.Visible = product.Visible;
            productInDb.Description = product.Description;

            await UnitOfWork.Complete();
        }

        public async Task MarkSaleProcessed(int saleId)
        {
            var saleInDb = await GetSaleById(saleId);

            if (saleInDb.Processed)
            {
                throw new ServiceException(ExceptionType.BadRequest, "Sale already processed");
            }

            saleInDb.Processed = true;

            await UnitOfWork.Complete();
        }
    }
}
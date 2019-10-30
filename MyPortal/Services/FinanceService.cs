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
            var product = await context.FinanceProducts.SingleOrDefaultAsync(x => x.Id == sale.ProductId);

            var student = await context.Students.SingleOrDefaultAsync(x => x.Id == sale.StudentId);

            if (product == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Product not found");
            }

            if (student == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Student not found");
            }

            if (product.Type.IsMeal && student.FreeSchoolMeals)
            {
                return true;
            }

            return student.AccountBalance >= product.Price;
        }

        public static async Task CheckoutBasketForStudent(int studentId, int academicYearId)
        {
            var student = await context.Students.SingleOrDefaultAsync(x => x.Id == studentId);

            if (student == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Student not found");
            }

            var basket = student.FinanceBasketItems;

            if (!basket.Any())
            {
                throw new ProcessException(ExceptionType.BadRequest, "There are no items in your basket");
            }

            if (basket.Sum(x => x.Product.Price) > student.AccountBalance)
            {
                throw new ProcessException(ExceptionType.Forbidden, "Insufficient funds");
            }

            //Process sales for each item
            foreach (var item in basket)
            {
                var sale = new FinanceSale
                {
                    StudentId = studentId,
                    ProductId = item.ProductId,
                    AcademicYearId = academicYearId
                };

                await CreateSale(sale, academicYearId, context, false);
            }

            context.FinanceBasketItems.RemoveRange(basket);

            await context.SaveChangesAsync();
        }

        public static async Task CreateBasketItem(FinanceBasketItem basketItem)
        {
            var student = await context.Students.SingleOrDefaultAsync(x => x.Id == basketItem.StudentId);

            if (student == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Student not found");
            }

            var product = await context.FinanceProducts.SingleOrDefaultAsync(x => x.Id == basketItem.ProductId);

            if (product == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Product not found");
            }

            if (!product.Visible)
            {
                throw new ProcessException(ExceptionType.Forbidden, "Product not available");
            }

            if ((student.FinanceBasketItems.Any(x => x.ProductId == basketItem.ProductId) ||
                 student.FinanceSales.Any(x => x.ProductId == basketItem.ProductId)) && product.OnceOnly)
            {
                throw new ProcessException(ExceptionType.Forbidden, "This product cannot be purchased more than once");
            }

            context.FinanceBasketItems.Add(basketItem);
            await context.SaveChangesAsync();
        }

        public static async Task CreateProduct(FinanceProduct product)
        {
            if (!ValidationService.ModelIsValid(product))
            {
                throw new ProcessException(ExceptionType.BadRequest, "Invalid data");
            }

            context.FinanceProducts.Add(product);
            await context.SaveChangesAsync();
        }

        public static async Task CreateSale(FinanceSale sale, int academicYearId, bool commitImmediately = true)
        {
            sale.Date = DateTime.Now;

            var student = await context.Students.SingleOrDefaultAsync(x => x.Id == sale.StudentId);

            var product = await context.FinanceProducts.SingleOrDefaultAsync(x => x.Id == sale.ProductId);

            if (student == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Student not found");
            }

            if (product == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Product not found");
            }

            if (product.Type.IsMeal)
            {
                sale.Processed = true;
            }

            if (product.Type.IsMeal && student.FreeSchoolMeals)
            {
                sale.AmountPaid = 0.00m;
                sale.AcademicYearId = academicYearId;

                context.FinanceSales.Add(sale);

                if (commitImmediately)
                {
                    await context.SaveChangesAsync();
                }
            }

            else
            {
                student.AccountBalance -= product.Price;

                sale.AmountPaid = product.Price;
                sale.AcademicYearId = academicYearId;

                context.FinanceSales.Add(sale);

                if (commitImmediately)
                {
                    context.SaveChanges();
                }
            }
        }

        public static async Task DeleteBasketItem(int basketItemId)
        {
            var itemInDb = await context.FinanceBasketItems.SingleOrDefaultAsync(x => x.Id == basketItemId);

            if (itemInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Item not found");
            }

            context.FinanceBasketItems.Remove(itemInDb);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteProduct(int productId)
        {
            var productInDb = await context.FinanceProducts.SingleOrDefaultAsync(p => p.Id == productId);

            if (productInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Product not found");
            }

            productInDb.Deleted = true;
            //context.FinanceProducts.Remove(productInDb); //Delete from database
            await context.SaveChangesAsync();
        }

        public static async Task DeleteSale(int saleId)
        {
            var saleInDb = await context.FinanceSales.SingleOrDefaultAsync(p => p.Id == saleId);

            if (saleInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Sale not found");
            }

            saleInDb.Deleted = true;
            //context.FinanceSales.Remove(saleInDb); //Delete from database
            await context.SaveChangesAsync();
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
            return await UnitOfWork.FinanceSales.GetPending();
        }

        public async Task<IEnumerable<FinanceSale>> GetProcessedSales(int academicYearId)
        {
            return await UnitOfWork.FinanceSales.GetProcessed();
        }

        public async Task<FinanceProduct> GetProductById(int productId)
        {
            var product = await UnitOfWork.FinanceProducts.GetByIdAsync(productId);

            if (product == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Product not found");
            }

            return product;
        }

        public async Task<FinanceSale> GetSaleById(int saleId)
        {
            var sale = await UnitOfWork.FinanceSales.GetByIdAsync(saleId);

            if (sale == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Sale not found");
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
            //ToDo: Write Method

            throw new NotImplementedException();
        }

        public static async Task ProcessManualTransaction(FinanceTransaction transaction, bool debit = false)
        {
            //if (transaction.Amount <= 0)
            //{
            //    throw new ProcessException(ExceptionType.BadRequest, "Amount cannot be negative");
            //}

            //var studentInDb = await context.Students.SingleOrDefaultAsync(s => s.Id == transaction.StudentId);

            //if (studentInDb == null)
            //{
            //    throw new ProcessException(ExceptionType.NotFound, "Student not found");
            //}

            //if (debit)
            //{
            //    transaction.Amount *= -1;
            //}

            //studentInDb.AccountBalance += transaction.Amount;

            //await context.SaveChangesAsync();

            //ToDo: Write Method

            throw new NotImplementedException();
        }

        public async Task RefundSale(int saleId)
        {
            var saleInDb = await GetSaleById(saleId);

            if (saleInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Sale not found");
            }

            saleInDb.Student.AccountBalance += saleInDb.AmountPaid;

            saleInDb.Processed = true;
            saleInDb.Refunded = true;
            await UnitOfWork.Complete();
        }

        public async Task UpdateProduct(FinanceProduct product)
        {
            if (product == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Product not found");
            }

            var productInDb = await GetProductById(product.Id);

            if (productInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Product not found");
            }

            productInDb.OnceOnly = product.OnceOnly;
            productInDb.Price = product.Price;
            productInDb.Visible = product.Visible;
            productInDb.Description = product.Description;

            await UnitOfWork.Complete();
        }

        public async Task MarkSaleProcessed(int saleId)
        {
            var saleInDb = GetS;

            if (saleInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Sale not found");
            }

            if (saleInDb.Processed)
            {
                throw new ProcessException(ExceptionType.BadRequest, "Sale already processed");
            }

            saleInDb.Processed = true;

            await context.SaveChangesAsync();
        }
    }
}
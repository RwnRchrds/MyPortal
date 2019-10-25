using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Exceptions;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.Services
{
    public static class FinanceService
    {
        public static async Task<bool> AssessBalance(FinanceSale sale, MyPortalDbContext context)
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

        public static async Task CheckoutBasketForStudent(int studentId, int academicYearId, MyPortalDbContext context)
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

        public static async Task CreateBasketItem(FinanceBasketItem basketItem, MyPortalDbContext context)
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

        public static async Task CreateProduct(FinanceProduct product, MyPortalDbContext context)
        {
            if (!ValidationService.ModelIsValid(product))
            {
                throw new ProcessException(ExceptionType.BadRequest, "Invalid data");
            }

            context.FinanceProducts.Add(product);
            await context.SaveChangesAsync();
        }

        public static async Task CreateSale(FinanceSale sale, int academicYearId, MyPortalDbContext context, bool commitImmediately = true)
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

        public static async Task DeleteBasketItem(int basketItemId, MyPortalDbContext context)
        {
            var itemInDb = await context.FinanceBasketItems.SingleOrDefaultAsync(x => x.Id == basketItemId);

            if (itemInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Item not found");
            }

            context.FinanceBasketItems.Remove(itemInDb);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteProduct(int productId, MyPortalDbContext context)
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

        public static async Task DeleteSale(int saleId, MyPortalDbContext context)
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

        public static async Task<IEnumerable<FinanceProductDto>> GetAllProducts(MyPortalDbContext context)
        {
            var products = await GetAllProductsModel(context);

            return products.Select(Mapper.Map<FinanceProduct, FinanceProductDto>);
        }

        public static async Task<IEnumerable<GridFinanceProductDto>> GetAllProductsDataGrid(MyPortalDbContext context)
        {
            var products = await GetAllProductsModel(context);

            return products.Select(Mapper.Map<FinanceProduct, GridFinanceProductDto>);
        }

        public static async Task<IDictionary<int, string>> GetAllProductsLookup(MyPortalDbContext context)
        {
            var products = await GetAllProductsModel(context);

            return products.ToDictionary(x => x.Id, x => x.Description);
        }

        public static async Task<IEnumerable<FinanceProduct>> GetAllProductsModel(MyPortalDbContext context)
        {
            return await context.FinanceProducts.Where(x => !x.Deleted).OrderBy(x => x.Description).ToListAsync();
        }

        public static async Task<IEnumerable<FinanceSaleDto>> GetAllSales(int academicYearId, MyPortalDbContext context)
        {
            var sales = await GetAllSalesModel(academicYearId, context);

            return sales.Select(Mapper.Map<FinanceSale, FinanceSaleDto>);
        }

        public static async Task<IEnumerable<GridFinanceSaleDto>> GetAllSalesDataGrid(int academicYearId, MyPortalDbContext context)
        {
            var sales = await GetAllSalesModel(academicYearId, context);

            return sales.Select(Mapper.Map<FinanceSale, GridFinanceSaleDto>);
        }

        public static async Task<IEnumerable<FinanceSale>> GetAllSalesModel(int academicYearId, MyPortalDbContext context)
        {
            return await context.FinanceSales.Where(x => !x.Deleted && x.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date).ToListAsync();
        }

        public static async Task<IEnumerable<FinanceSaleDto>> GetAllSalesByStudent(int studentId,
            int academicYearId, MyPortalDbContext context)
        {
            var sales = await context.FinanceSales
                .Where(x => !x.Deleted && x.StudentId == studentId && x.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date).ToListAsync();
            
            return sales.Select(Mapper.Map<FinanceSale, FinanceSaleDto>);
        }

        public static async Task<IEnumerable<FinanceProductDto>> GetAvailableProductsByStudent(int studentId,
            MyPortalDbContext context)
        {
            var items = await context.FinanceProducts
                .Where(x => !x.Deleted && (x.OnceOnly && x.Visible ||
                                           x.BasketItems.All(i => i.StudentId != studentId) &&
                                           x.Sales.All(s => s.StudentId != studentId)))
                .OrderBy(x => x.Description).ToListAsync();
                
            return items.Select(Mapper.Map<FinanceProduct, FinanceProductDto>);
        }

        public static async Task<IEnumerable<FinanceBasketItemDto>> GetBasketItemsByStudent(int studentId, MyPortalDbContext context)
        {
            var basketItems = await context.FinanceBasketItems.Where(x => x.StudentId == studentId)
                .OrderBy(x => x.Product.Description).ToListAsync();
            
            return basketItems.Select(Mapper.Map<FinanceBasketItem, FinanceBasketItemDto>);
        }

        public static async Task<decimal> GetBasketTotalForStudent(int studentId, MyPortalDbContext context)
        {
            var total = await context.FinanceBasketItems.Where(x => x.StudentId == studentId)
                .SumAsync(x => x.Product.Price);

            return total;
        }
        
        public static async Task<IEnumerable<FinanceSaleDto>> GetPendingSales(int academicYearId,
            MyPortalDbContext context)
        {
            var sales = await GetPendingSalesModel(academicYearId, context);

            return sales.Select(Mapper.Map<FinanceSale, FinanceSaleDto>);
        }

        public static async Task<IEnumerable<GridFinanceSaleDto>> GetPendingSalesDataGrid(int academicYearId,
            MyPortalDbContext context)
        {
            var sales = await GetPendingSalesModel(academicYearId, context);

            return sales.Select(Mapper.Map<FinanceSale, GridFinanceSaleDto>);
        }

        public static async Task<IEnumerable<FinanceSale>> GetPendingSalesModel(int academicYearId,
            MyPortalDbContext context)
        {
            return await context.FinanceSales
                .Where(x => !x.Deleted && !x.Processed && x.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date).ToListAsync();
        }

        public static async Task<IEnumerable<FinanceSaleDto>> GetProcessedSales(int academicYearId, MyPortalDbContext context)
        {
            var sales = await GetProcessedSalesModel(academicYearId, context);

            return sales.Select(Mapper.Map<FinanceSale, FinanceSaleDto>);
        }

        public static async Task<IEnumerable<GridFinanceSaleDto>> GetProcessedSalesDataGrid(int academicYearId, MyPortalDbContext context)
        {
            var sales = await GetProcessedSalesModel(academicYearId, context);

            return sales.Select(Mapper.Map<FinanceSale, GridFinanceSaleDto>);
        }

        public static async Task<IEnumerable<FinanceSale>> GetProcessedSalesModel(int academicYearId, MyPortalDbContext context)
        {
            return await context.FinanceSales
                .Where(x => !x.Deleted && x.Processed && x.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date).ToListAsync();
        }

        public static async Task<FinanceProductDto> GetProductById(int productId, MyPortalDbContext context)
        {
            var product = await context.FinanceProducts.SingleOrDefaultAsync(x => x.Id == productId);

            if (product == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Product not found");
            }

            return Mapper.Map<FinanceProduct, FinanceProductDto>(product);
        }

        public static async Task<decimal> GetProductPrice(int productId, MyPortalDbContext context)
        {
            var productInDb = await context.FinanceProducts.SingleOrDefaultAsync(x => x.Id == productId);

            if (productInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Product not found");
            }

            return productInDb.Price;
        }

        public static async Task<IEnumerable<FinanceProductType>> GetAllProductTypesModel(MyPortalDbContext context)
        {
            return await context.FinanceProductTypes.OrderBy(x => x.Description).ToListAsync();
        }

        public static async Task<IDictionary<int, string>> GetAllProductTypesLookup(MyPortalDbContext context)
        {
            var productTypes = await GetAllProductTypesModel(context);

            return productTypes.ToDictionary(x => x.Id, x => x.Description);
        }

        public static async Task<decimal> GetStudentBalance(int studentId, MyPortalDbContext context)
        {
            var studentInDb = await context.Students.SingleOrDefaultAsync(x => x.Id == studentId);

            if (studentInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Student not found");
            }

            return studentInDb.AccountBalance;
        }

        public static async Task ProcessManualTransaction(FinanceTransaction transaction,
            MyPortalDbContext context, bool debit = false)
        {
            if (transaction.Amount <= 0)
            {
                throw new ProcessException(ExceptionType.BadRequest, "Amount cannot be negative");
            }

            var studentInDb = await context.Students.SingleOrDefaultAsync(s => s.Id == transaction.StudentId);

            if (studentInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Student not found");
            }

            if (debit)
            {
                transaction.Amount *= -1;
            }

            studentInDb.AccountBalance += transaction.Amount;

            await context.SaveChangesAsync();
        }

        public static async Task RefundSale(int saleId, MyPortalDbContext context)
        {
            var saleInDb = await context.FinanceSales.SingleOrDefaultAsync(p => p.Id == saleId);

            if (saleInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Sale not found");
            }

            saleInDb.Student.AccountBalance += saleInDb.AmountPaid;

            saleInDb.Processed = true;
            saleInDb.Refunded = true;
            await context.SaveChangesAsync();
        }

        public static async Task UpdateProduct(FinanceProduct product, MyPortalDbContext context)
        {
            if (product == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Product not found");
            }

            var productInDb = await context.FinanceProducts.SingleOrDefaultAsync(x => x.Id == product.Id);

            if (productInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Product not found");
            }

            productInDb.OnceOnly = product.OnceOnly;
            productInDb.Price = product.Price;
            productInDb.Visible = product.Visible;
            productInDb.Description = product.Description;

            await context.SaveChangesAsync();
        }

        public static async Task MarkSaleProcessed(int saleId, MyPortalDbContext context)
        {
            var saleInDb = await context.FinanceSales.SingleOrDefaultAsync(x => x.Id == saleId);

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
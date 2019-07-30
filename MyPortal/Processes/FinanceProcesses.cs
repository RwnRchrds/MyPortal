using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class FinanceProcesses
    {
        public static ProcessResponse<object> CreateBasketItem(FinanceBasketItem basketItem, MyPortalDbContext context)
        {
            var student = context.Students.SingleOrDefault(x => x.Id == basketItem.StudentId);

            if (student == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Student not found", null);
            }

            var product = context.FinanceProducts.SingleOrDefault(x => x.Id == basketItem.ProductId);

            if (product == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Product not found", null);
            }

            if (!product.Visible)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Product not available", null);
            }

            if (context.FinanceSales.Any(x =>
                x.StudentId == basketItem.StudentId && x.ProductId == basketItem.ProductId && x.FinanceProduct.OnceOnly) || context.FinanceBasketItems.Any(x =>
                    x.StudentId == basketItem.StudentId && x.ProductId == basketItem.ProductId && x.FinanceProduct.OnceOnly))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "This product cannot be purchased more than once", null);
            }

            context.FinanceBasketItems.Add(basketItem);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Item added to basket", null);
        }

        public static ProcessResponse<IEnumerable<FinanceBasketItemDto>> GetBasketItemsForStudent(int studentId, MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<FinanceBasketItemDto>>(ResponseType.Ok, null,
                context.FinanceBasketItems
                    .Where(x => x.StudentId == studentId)
                    .OrderBy(x => x.FinanceProduct.Description)
                    .ToList()
                    .Select(Mapper.Map<FinanceBasketItem, FinanceBasketItemDto>));
        }

        public static ProcessResponse<decimal> GetBasketTotalForStudent(int studentId, MyPortalDbContext context)
        {
            var allItems = context.FinanceBasketItems.Where(x => x.StudentId == studentId);

            var total = allItems.Sum(x => x.FinanceProduct.Price);

            return new ProcessResponse<decimal>(ResponseType.Ok, null, total);
        }

        public static ProcessResponse<object> DeleteBasketItem(int basketItemId, MyPortalDbContext context)
        {
            var itemInDb = context.FinanceBasketItems.SingleOrDefault(x => x.Id == basketItemId);

            if (itemInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Basket item not found", null);
            }

            context.FinanceBasketItems.Remove(itemInDb);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Item removed from basket", null);
        }

        public static ProcessResponse<object> DeleteProduct(int productId, MyPortalDbContext context)
        {
            var productInDb = context.FinanceProducts.SingleOrDefault(p => p.Id == productId);

            if (productInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Product not found", null);
            }

            productInDb.Deleted = true;
            //context.FinanceProducts.Remove(productInDb); //Delete from database
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Product deleted", null);
        }

        public static ProcessResponse<IEnumerable<FinanceProductDto>> GetAvailableProductsForStudent(int studentId,
            MyPortalDbContext context)
        {
            var items = context.FinanceProducts
                .Where(x => !x.Deleted && (x.OnceOnly && x.Visible ||
                                           x.FinanceBasketItems.All(i => i.StudentId != studentId) &&
                                           x.FinanceSales.All(s => s.StudentId != studentId)))
                .OrderBy(x => x.Description)
                .ToList()
                .Select(Mapper.Map<FinanceProduct, FinanceProductDto>);

            return new ProcessResponse<IEnumerable<FinanceProductDto>>(ResponseType.Ok, null, items);
        }

        public static ProcessResponse<decimal> GetProductPrice(int productId, MyPortalDbContext context)
        {
            var productInDb = context.FinanceProducts.Single(x => x.Id == productId);

            if (productInDb == null)
            {
                return new ProcessResponse<decimal>(ResponseType.NotFound, "Product not found", 0);
            }

            return new ProcessResponse<decimal>(ResponseType.Ok, null, productInDb.Price);
        }

        public static ProcessResponse<FinanceProductDto> GetProductById(int productId, MyPortalDbContext context)
        {
            var product = context.FinanceProducts.SingleOrDefault(x => x.Id == productId);

            if (product == null)
            {
                return new ProcessResponse<FinanceProductDto>(ResponseType.NotFound, "Product not found", null);
            }

            return new ProcessResponse<FinanceProductDto>(ResponseType.Ok, null,
                Mapper.Map<FinanceProduct, FinanceProductDto>(product));
        }

        public static ProcessResponse<IEnumerable<FinanceProduct>> GetAllProducts_Model(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<FinanceProduct>>(ResponseType.Ok, null, context.FinanceProducts
                .Where(x => !x.Deleted)
                .OrderBy(x => x.Description)
                .ToList());
        }

        public static ProcessResponse<IEnumerable<FinanceProductDto>> GetAllProducts(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<FinanceProductDto>>(ResponseType.Ok, null,
                GetAllProducts_Model(context).ResponseObject.Select(Mapper.Map<FinanceProduct, FinanceProductDto>));
        }

        public static ProcessResponse<IEnumerable<GridFinanceProductDto>> GetAllProducts_DataGrid(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridFinanceProductDto>>(ResponseType.Ok, null,
                GetAllProducts_Model(context).ResponseObject.Select(Mapper.Map<FinanceProduct, GridFinanceProductDto>));

        }

        public static ProcessResponse<object> CreateProduct(FinanceProduct product, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(product))
            {
               return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            context.FinanceProducts.Add(product);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Product created", null);
        }

        public static ProcessResponse<object> UpdateProduct(FinanceProduct product, MyPortalDbContext context)
        {
            if (product == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Product not found", null);
            }

            var productInDb = context.FinanceProducts.SingleOrDefault(x => x.Id == product.Id);

            if (productInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Product not found", null);
            }

            productInDb.OnceOnly = product.OnceOnly;
            productInDb.Price = product.Price;
            productInDb.Visible = product.Visible;
            productInDb.Description = product.Description;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Product updated", null);
        }

        public static ProcessResponse<bool> AssessBalance(FinanceSale sale, MyPortalDbContext context)
        {
            var product = context.FinanceProducts.SingleOrDefault(x => x.Id == sale.ProductId);

            var student = context.Students.SingleOrDefault(x => x.Id == sale.StudentId);

            if (product == null)
            {
                return new ProcessResponse<bool>(ResponseType.NotFound, "Product not found", false);
            }

            if (student == null)
            {
                return new ProcessResponse<bool>(ResponseType.NotFound, "Student not found", false);
            }

            if (student.FreeSchoolMeals && product.FinanceProductType.IsMeal)
            {
                return new ProcessResponse<bool>(ResponseType.Ok, null, true);
            }

            return new ProcessResponse<bool>(ResponseType.Ok, null, student.AccountBalance >= product.Price);
        }

        public static ProcessResponse<object> DeleteSale(int saleId, MyPortalDbContext context)
        {
            var saleInDb = context.FinanceSales.SingleOrDefault(p => p.Id == saleId);

            if (saleInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Sale not found", null);
            }

            saleInDb.Deleted = true;
            //context.FinanceSales.Remove(saleInDb); //Delete from database
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Sale deleted", null);
        }

        public static ProcessResponse<IEnumerable<FinanceSale>> GetProcessedSales_Model(int academicYearId, MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<FinanceSale>>(ResponseType.Ok, null, context.FinanceSales
                .Where(x => !x.Deleted && x.Processed && x.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date)
                .ToList());
        }

        public static ProcessResponse<IEnumerable<FinanceSaleDto>> GetProcessedSales(int academicYearId, MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<FinanceSaleDto>>(ResponseType.Ok, null,
                GetProcessedSales_Model(academicYearId, context).ResponseObject
                    .Select(Mapper.Map<FinanceSale, FinanceSaleDto>));
        }

        public static ProcessResponse<IEnumerable<GridFinanceSaleDto>> GetProcessedSales_DataGrid(int academicYearId, MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridFinanceSaleDto>>(ResponseType.Ok, null,
                GetProcessedSales_Model(academicYearId, context).ResponseObject
                    .Select(Mapper.Map<FinanceSale, GridFinanceSaleDto>));
        }

        public static ProcessResponse<IEnumerable<FinanceSale>> GetAllSales_Model(int academicYearId, MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<FinanceSale>>(ResponseType.Ok, null, context.FinanceSales
                .Where(x => !x.Deleted && x.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date)
                .ToList());
        }

        public static ProcessResponse<IEnumerable<FinanceSaleDto>> GetAllSales(int academicYearId, MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<FinanceSaleDto>>(ResponseType.Ok, null,
                GetAllSales_Model(academicYearId, context).ResponseObject
                    .Select(Mapper.Map<FinanceSale, FinanceSaleDto>));
        }

        public static ProcessResponse<IEnumerable<GridFinanceSaleDto>> GetAllSales_DataGrid(int academicYearId, MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridFinanceSaleDto>>(ResponseType.Ok, null,
                GetAllSales_Model(academicYearId, context).ResponseObject
                    .Select(Mapper.Map<FinanceSale, GridFinanceSaleDto>));
        }

        public static ProcessResponse<IEnumerable<FinanceSaleDto>> GetAllSalesForStudent(int studentId,
            int academicYearId, MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<FinanceSaleDto>>(ResponseType.Ok, null, context.FinanceSales
                .Where(x => !x.Deleted && x.StudentId == studentId && x.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<FinanceSale, FinanceSaleDto>));
        }

        public static ProcessResponse<IEnumerable<FinanceSale>> GetPendingSales_Model(int academicYearId,
            MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<FinanceSale>>(ResponseType.Ok, null, context.FinanceSales
                .Where(x => !x.Deleted && !x.Processed && x.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date)
                .ToList());
        }

        public static ProcessResponse<IEnumerable<FinanceSaleDto>> GetPendingSales(int academicYearId,
            MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<FinanceSaleDto>>(ResponseType.Ok, null,
                GetPendingSales_Model(academicYearId, context).ResponseObject
                    .Select(Mapper.Map<FinanceSale, FinanceSaleDto>));
        }

        public static ProcessResponse<IEnumerable<GridFinanceSaleDto>> GetPendingSales_DataGrid(int academicYearId,
            MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridFinanceSaleDto>>(ResponseType.Ok, null,
                GetPendingSales_Model(academicYearId, context).ResponseObject
                    .Select(Mapper.Map<FinanceSale, GridFinanceSaleDto>));
        }

        public static ProcessResponse<object> CreateSale(FinanceSale sale, int academicYearId, MyPortalDbContext context, bool commitImmediately = true)
        {
            
            sale.Date = DateTime.Now;

            sale.Processed = true;

            var student = context.Students.SingleOrDefault(x => x.Id == sale.StudentId);

            var product = context.FinanceProducts.SingleOrDefault(x => x.Id == sale.ProductId);

            if (student == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Student not found", null);
            }

            if (product == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Product not found", null);
            }

            if (student.FreeSchoolMeals && product.FinanceProductType.IsMeal)
            {
                sale.AmountPaid = 0.00m;
                sale.AcademicYearId = academicYearId;

                context.FinanceSales.Add(sale);

                if (commitImmediately)
                {
                    context.SaveChanges();
                }

                return new ProcessResponse<object>(ResponseType.Ok, "Sale completed", null);
            }

            student.AccountBalance -= product.Price;

            sale.AmountPaid = product.Price;
            sale.AcademicYearId = academicYearId;

            context.FinanceSales.Add(sale);

            if (commitImmediately)
            { 
                context.SaveChanges();
            }

            return new ProcessResponse<object>(ResponseType.Ok, "Sale completed", null);
        }

        public static ProcessResponse<object> CheckoutBasketForStudent(int studentId, int academicYearId, MyPortalDbContext context)
        {
            var student = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (student == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Student not found", null);
            }

            var basket = context.FinanceBasketItems.Where(x => x.StudentId == studentId);

            if (!basket.Any())
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "There are no items in your basket", null);
            }

            if (basket.Sum(x => x.FinanceProduct.Price) > student.AccountBalance)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Insufficient funds", null);
            }

            //Process sales for each item
            foreach (var item in basket)
            {
                var sale = new FinanceSale
                {
                    StudentId = studentId,
                    ProductId = item.ProductId,
                    Date = DateTime.Today,
                    AcademicYearId = academicYearId
                };

                CreateSale(sale, academicYearId, context, false);
            }

            context.FinanceBasketItems.RemoveRange(basket);

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Purchase successful", null);
        }

        public static ProcessResponse<object> RefundSale(int saleId, MyPortalDbContext context)
        {
            var saleInDb = context.FinanceSales.SingleOrDefault(p => p.Id == saleId);

            if (saleInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Sale not found", null);
            }

            saleInDb.CoreStudent.AccountBalance += saleInDb.AmountPaid;

            saleInDb.Processed = true;
            saleInDb.Refunded = true;
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Sale refunded", null);
        }

        public static ProcessResponse<object> ProcessManualTransaction(FinanceTransaction transaction,
            MyPortalDbContext context, bool credit = false)
        {
            if (transaction.Amount <= 0)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Amount cannot be negative", null);
            }

            var studentInDb = context.Students.SingleOrDefault(s => s.Id == transaction.StudentId);

            if (studentInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Student not found", null);
            }

            if (!credit)
            {
                transaction.Amount = transaction.Amount * -1;
            }

            studentInDb.AccountBalance += transaction.Amount;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Transaction completed", null);
        }

        public static ProcessResponse<decimal> GetStudentBalance(int studentId, MyPortalDbContext context)
        {
            var studentInDb = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (studentInDb == null)
            {
                return new ProcessResponse<decimal>(ResponseType.NotFound, "Student not found", 0);
            }

            return new ProcessResponse<decimal>(ResponseType.Ok, null, studentInDb.AccountBalance);
        }

        public static ProcessResponse<IEnumerable<FinanceProductType>> GetProductTypes_Model(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<FinanceProductType>>(ResponseType.Ok, null,
                context.FinanceProductTypes.ToList().OrderBy(x => x.Description));
        }
    }
}
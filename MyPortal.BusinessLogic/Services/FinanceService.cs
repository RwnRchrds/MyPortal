using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.BusinessLogic.Models;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Services
{
    public class FinanceService : MyPortalService
    {
        public FinanceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        public FinanceService() : base()
        {

        }

        public async Task<bool> AssessBalance(Sale sale)
        {
            using (var studentService = new StudentService())
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
                    await CreateSale(new Sale
                    {
                        AcademicYearId = academicYearId,
                        StudentId = studentId,
                        ProductId = item.ProductId
                    }, academicYearId, false);
                }

                UnitOfWork.BasketItems.RemoveRange(basket);

                await UnitOfWork.Complete();   
            }
        }

        public async Task CreateBasketItem(BasketItem basketItem)
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
                     student.Sales.Any(x => x.ProductId == basketItem.ProductId)) && product.OnceOnly)
                {
                    throw new ServiceException(ExceptionType.Forbidden, "This product cannot be purchased more than once");
                }

                UnitOfWork.BasketItems.Add(basketItem);
                await UnitOfWork.Complete();
            }
        }

        public async Task CreateProduct(Product product)
        {
            ValidationService.ValidateModel(product);

            UnitOfWork.Products.Add(product);
            await UnitOfWork.Complete();
        }

        public async Task CreateSale(Sale sale, int academicYearId, bool commitImmediately = true)
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

                    UnitOfWork.Sales.Add(sale);

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

                    UnitOfWork.Sales.Add(sale);

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

            UnitOfWork.BasketItems.Remove(itemInDb);
            await UnitOfWork.Complete();
        }

        public async Task DeleteProduct(int productId)
        {
            var productInDb = await GetProductById(productId);

            UnitOfWork.Products.Remove(productInDb); //Delete from database

            await UnitOfWork.Complete();
        }

        public async Task DeleteSale(int saleId)
        {
            var saleInDb = await GetSaleById(saleId);

            UnitOfWork.Sales.Remove(saleInDb); //Delete from database

            await UnitOfWork.Complete();
        }

        public async Task<IDictionary<int, string>> GetAllProductsLookup()
        {
            var products = await GetAllProducts();

            return products.ToDictionary(x => x.Id, x => x.Description);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await UnitOfWork.Products.GetAll();
        }

        public async Task<IEnumerable<Sale>> GetAllSales(int academicYearId)
        {
            return await UnitOfWork.Sales.GetAllAsync(academicYearId);
        }

        public async Task<IEnumerable<Sale>> GetAllSalesByStudent(int studentId,
            int academicYearId)
        {
            var sales = await UnitOfWork.Sales.GetByStudent(studentId, academicYearId);
            
            return sales;
        }

        public async Task<IEnumerable<Product>> GetAvailableProductsByStudent(int studentId)
        {
            var items = await UnitOfWork.Products.GetAvailableByStudent(studentId);

            return items;
        }

        public async Task<BasketItem> GetBasketItemById(int basketItemId)
        {
            var item = await UnitOfWork.BasketItems.GetById(basketItemId);

            if (item == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Item not found");
            }

            return item;
        }

        public async Task<IEnumerable<BasketItem>> GetBasketItemsByStudent(int studentId)
        {
            var basketItems = await UnitOfWork.BasketItems.GetByStudent(studentId);

            return basketItems;
        }

        public async Task<decimal> GetBasketTotalForStudent(int studentId)
        {
            var total = await UnitOfWork.BasketItems.GetTotalForStudent(studentId);

            return total;
        }

        public async Task<IEnumerable<Sale>> GetPendingSales(int academicYearId)
        {
            return await UnitOfWork.Sales.GetPending(academicYearId);
        }

        public async Task<IEnumerable<Sale>> GetProcessedSales(int academicYearId)
        {
            return await UnitOfWork.Sales.GetProcessed(academicYearId);
        }

        public async Task<Product> GetProductById(int productId)
        {
            var product = await UnitOfWork.Products.GetById(productId);

            if (product == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Product not found");
            }

            return product;
        }

        public async Task<Sale> GetSaleById(int saleId)
        {
            var sale = await UnitOfWork.Sales.GetById(saleId);

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

        public async Task<IEnumerable<ProductType>> GetAllProductTypes()
        {
            return await UnitOfWork.ProductTypes.GetAll();
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

        public async Task UpdateProduct(Product product)
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
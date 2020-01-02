using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.BusinessLogic.Models;
using MyPortal.BusinessLogic.Models.Data;
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

        public async Task<bool> AssessBalance(SaleDto sale)
        {
            using (var studentService = new StudentService())
            {
                var product = await GetProductById(sale.ProductId);

                var student = await studentService.GetStudentById(sale.StudentId);

                if (product == null)
                {
                    throw new ServiceException(ExceptionType.NotFound, "Product not found.");
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
            var student = await UnitOfWork.Students.GetById(studentId);

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
                await CreateSale(new SaleDto
                {
                    AcademicYearId = academicYearId,
                    StudentId = studentId,
                    ProductId = item.ProductId
                }, academicYearId, false);
            }

            UnitOfWork.BasketItems.RemoveRange(basket);

            await UnitOfWork.Complete();
        }

        public async Task CreateBasketItem(BasketItemDto basketItem)
        {
            var student = await UnitOfWork.Students.GetById(basketItem.StudentId);

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

            UnitOfWork.BasketItems.Add(Mapper.Map<BasketItem>(basketItem));
            await UnitOfWork.Complete();
        }

        public async Task CreateProduct(ProductDto product)
        {
            ValidationService.ValidateModel(product);

            UnitOfWork.Products.Add(Mapper.Map<Product>(product));
            await UnitOfWork.Complete();
        }

        public async Task CreateSale(SaleDto sale, int academicYearId, bool commitImmediately = true)
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

                    UnitOfWork.Sales.Add(Mapper.Map<Sale>(sale));

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

                    UnitOfWork.Sales.Add(Mapper.Map<Sale>(sale));

                    if (commitImmediately)
                    {
                        await UnitOfWork.Complete();
                    }
                }   
            }
        }

        public async Task DeleteBasketItem(int basketItemId)
        {
            var itemInDb = await UnitOfWork.BasketItems.GetById(basketItemId);

            if (itemInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Item not found in basket.");
            }

            UnitOfWork.BasketItems.Remove(itemInDb);
            await UnitOfWork.Complete();
        }

        public async Task DeleteProduct(int productId)
        {
            var productInDb = await UnitOfWork.Products.GetById(productId);

            if (productInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Product not found");
            }

            UnitOfWork.Products.Remove(productInDb); //Delete from database

            await UnitOfWork.Complete();
        }

        public async Task DeleteSale(int saleId)
        {
            var saleInDb = await UnitOfWork.Sales.GetById(saleId);

            if (saleInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Sale not found");
            }

            UnitOfWork.Sales.Remove(saleInDb); //Delete from database

            await UnitOfWork.Complete();
        }

        public async Task<IDictionary<int, string>> GetAllProductsLookup()
        {
            return (await GetAllProducts()).ToDictionary(x => x.Id, x => x.Description);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            return (await UnitOfWork.Products.GetAll()).Select(Mapper.Map<ProductDto>);
        }

        public async Task<IEnumerable<SaleDto>> GetAllSales(int academicYearId)
        {
            return (await UnitOfWork.Sales.GetAllAsync(academicYearId)).Select(Mapper.Map<SaleDto>);
        }

        public async Task<IEnumerable<SaleDto>> GetAllSalesByStudent(int studentId,
            int academicYearId)
        {
            return (await UnitOfWork.Sales.GetByStudent(studentId, academicYearId)).Select(Mapper.Map<SaleDto>);
        }

        public async Task<IEnumerable<ProductDto>> GetAvailableProductsByStudent(int studentId)
        {
            return (await UnitOfWork.Products.GetAvailableByStudent(studentId)).Select(Mapper.Map<ProductDto>);
        }

        public async Task<BasketItemDto> GetBasketItemById(int basketItemId)
        {
            var item = await UnitOfWork.BasketItems.GetById(basketItemId);

            if (item == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Item not found");
            }

            return Mapper.Map<BasketItemDto>(item);
        }

        public async Task<IEnumerable<BasketItemDto>> GetBasketItemsByStudent(int studentId)
        {
            return (await UnitOfWork.BasketItems.GetByStudent(studentId)).Select(Mapper.Map<BasketItemDto>);
        }

        public async Task<decimal> GetBasketTotalForStudent(int studentId)
        {
            return await UnitOfWork.BasketItems.GetTotalForStudent(studentId);
        }

        public async Task<IEnumerable<SaleDto>> GetPendingSales(int academicYearId)
        {
            return (await UnitOfWork.Sales.GetPending(academicYearId)).Select(Mapper.Map<SaleDto>);
        }

        public async Task<IEnumerable<SaleDto>> GetProcessedSales(int academicYearId)
        {
            return (await UnitOfWork.Sales.GetProcessed(academicYearId)).Select(Mapper.Map<SaleDto>);
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            var product = await UnitOfWork.Products.GetById(productId);

            if (product == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Product not found");
            }

            return Mapper.Map<ProductDto>(product);
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

        public async Task<IEnumerable<ProductTypeDto>> GetAllProductTypes()
        {
            return (await UnitOfWork.ProductTypes.GetAll()).Select(Mapper.Map<ProductTypeDto>);
        }

        public async Task<IDictionary<int, string>> GetAllProductTypesLookup()
        {
            return (await GetAllProductTypes()).ToDictionary(x => x.Id, x => x.Description);
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

        public async Task UpdateProduct(ProductDto product)
        {
            var productInDb = await UnitOfWork.Products.GetById(product.Id);

            if (productInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Product not found");
            }

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
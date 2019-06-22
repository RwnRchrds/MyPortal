using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using MyPortal.Processes;

namespace MyPortal.Controllers.Api
{
    public class FinanceController : MyPortalApiController
    {
        #region Basket Items
        /// <summary>
/// Adds a basket item to student's basket.
/// </summary>
/// <param name="data">The basket item to add to the database</param>
/// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/basket/add")]
        public IHttpActionResult AddToBasket(FinanceBasketItem data)
        {
            var studentQuery = _context.Students.SingleOrDefault(x => x.Id == data.StudentId);

            if (studentQuery == null)
            {
                return Content(HttpStatusCode.NotFound, "Student not found");
            }

            var productToAdd = _context.FinanceProducts.SingleOrDefault(x => x.Id == data.ProductId);

            if (productToAdd == null)
            {
                return Content(HttpStatusCode.NotFound, "Product not found");
            }

            if (!productToAdd.Visible)
            {
                return Content(HttpStatusCode.BadRequest, "Product not available");
            }

            var purchased =
                _context.FinanceSales.Where(x =>
                    x.StudentId == data.StudentId && x.ProductId == data.ProductId && x.FinanceProduct.OnceOnly);

            var inBasket =
                _context.FinanceBasketItems.Where(x =>
                    x.StudentId == data.StudentId && x.ProductId == data.ProductId && x.FinanceProduct.OnceOnly);

            if (purchased.Any() || inBasket.Any())
            {
                return Content(HttpStatusCode.BadRequest, "This product cannot be purchased more than once");
            }

            var itemToAdd = new FinanceBasketItem
            {
                ProductId = data.ProductId,
                StudentId = data.StudentId
            };

            _context.FinanceBasketItems.Add(itemToAdd);
            _context.SaveChanges();

            return Ok("Item added to basket");
        }

/// <summary>
/// Gets a list of basket items for the specified student.
/// </summary>
/// <param name="student">The ID of the student to fetch basket items for.</param>
/// <returns>Returns a list of DTOs of basket items for the student.</returns>
        [HttpGet]
        [Route("api/basket")]
        public IEnumerable<FinanceBasketItemDto> GetBasketItems(int student)
        {
            return _context.FinanceBasketItems
                .Where(x => x.StudentId == student)
                .OrderBy(x => x.FinanceProduct.Description)
                .ToList()
                .Select(Mapper.Map<FinanceBasketItem, FinanceBasketItemDto>);
        }

/// <summary>
/// Gets the total price of all the items in the specified student's basket.
/// </summary>
/// <param name="student"></param>
/// <returns>Returns a decimal of the total price.</returns>
        [HttpGet]
        [Route("api/basket/total")]
        public decimal GetTotal(int student)
        {
            var allItems = _context.FinanceBasketItems.Where(x => x.StudentId == student);

            if (!allItems.Any())
            {
                return 0.00m;
            }

            var total = allItems.Sum(x => x.FinanceProduct.Price);

            return total;
        }

/// <summary>
/// Removes a basket item from the student's basket.
/// </summary>
/// <param name="id">The ID of the item to remove from the basket.</param>
/// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("api/basket/remove/{id}")]
        public IHttpActionResult RemoveFromBasket(int id)
        {
            var itemInDb = _context.FinanceBasketItems.SingleOrDefault(x => x.Id == id);

            if (itemInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Item not found");
            }

            _context.FinanceBasketItems.Remove(itemInDb);
            _context.SaveChanges();

            return Ok("Item removed from basket");
        }
        #endregion

        #region Products
        [HttpDelete]
        [Route("api/products/{id}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            var productInDb = _context.FinanceProducts.SingleOrDefault(p => p.Id == id);

            if (productInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Product not found");
            }

            _context.FinanceProducts.Remove(productInDb);
            _context.SaveChanges();

            return Ok("Product deleted");
        }

        /// <summary>
        /// Gets the products available to buy for the specified student.
        /// </summary>
        /// <param name="student">The ID of the student to get products for.</param>
        /// <returns>Returns a list of DTOs of products available to buy for the specified student.</returns>
        [HttpGet]
        [Route("api/products/store")]
        public IEnumerable<FinanceProductDto> GetAvailableProducts(int student)
        {
            var purchased = _context.FinanceSales.Where(a => a.StudentId == student);

            var inBasket = _context.FinanceBasketItems.Where(a => a.StudentId == student);

            return _context.FinanceProducts
                .Where(x => !x.OnceOnly && x.Visible || x.Visible && purchased.All(p => p.ProductId != x.Id) &&
                            inBasket.All(b => b.ProductId != x.Id))
                .OrderBy(x => x.Description)
                .ToList()
                .Select(Mapper.Map<FinanceProduct, FinanceProductDto>);
        }

        /// <summary>
        /// Gets the price of the specified product.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <returns>Returns a decimal of the price of the product.</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        [Route("api/products/price/{productId}")]
        public decimal GetPrice(int productId)
        {
            var productInDb = _context.FinanceProducts.Single(x => x.Id == productId);

            if (productInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return productInDb.Price;
        }

        /// <summary>
        /// Get the specified product.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>Returns a DTO of the specified product</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        [Route("api/products/{id}")]
        public FinanceProductDto GetProduct(int id)
        {
            var product = _context.FinanceProducts.SingleOrDefault(x => x.Id == id);

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<FinanceProduct, FinanceProductDto>(product);
        }

        /// <summary>
        /// Gets a list of all products.
        /// </summary>
        /// <returns>Returns a list of DTOs of all products.</returns>
        [HttpGet]
        [Route("api/products")]
        public IEnumerable<FinanceProductDto> GetProducts()
        {
            return _context.FinanceProducts
                .OrderBy(x => x.Description)
                .ToList()
                .Select(Mapper.Map<FinanceProduct, FinanceProductDto>);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/products/new")]
        public IHttpActionResult NewProduct(FinanceProduct product)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid product");
            }

            _context.FinanceProducts.Add(product);
            _context.SaveChanges();

            return Ok("Product added");
        }

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/products/edit")]
        public IHttpActionResult UpdateProduct(FinanceProduct product)
        {
            if (product == null)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var productInDb = _context.FinanceProducts.SingleOrDefault(x => x.Id == product.Id);

            if (productInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Product not found");
            }

            productInDb.OnceOnly = product.OnceOnly;
            productInDb.Price = product.Price;
            productInDb.Visible = product.Visible;
            productInDb.Description = product.Description;

            _context.SaveChanges();

            return Ok("Product updated");
        }

        #endregion

        #region Sales
        /// <summary>
        /// Checks whether the student has enough funds to purchase an item.
        /// </summary>
        /// <param name="sale">The sale with the</param>
        /// <returns>Returns a boolean value indicating whether the student has enough funds to purchase the item.</returns>
        /// <exception cref="HttpResponseException">Thrown when the product or student is not found.</exception>
        [HttpPost]
        [Route("api/sales/query")]
        public bool AssessBalance(FinanceSaleDto sale)
        {
            var productToQuery = _context.FinanceProducts.SingleOrDefault(x => x.Id == sale.ProductId);

            var studentToQuery = _context.Students.SingleOrDefault(x => x.Id == sale.StudentId);

            if (productToQuery == null || studentToQuery == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (studentToQuery.FreeSchoolMeals && productToQuery.FinanceProductType.IsMeal)
            {
                return true;
            }

            return studentToQuery.AccountBalance >= productToQuery.Price;
        }

        /// <summary>
        /// Deletes the specified sale.
        /// </summary>
        /// <param name="id">The ID of the sale to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("api/sales/delete/{id}")]
        public IHttpActionResult DeleteSale(int id)
        {
            var saleInDb = _context.FinanceSales.SingleOrDefault(p => p.Id == id);

            if (saleInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Sale not found");
            }

            _context.FinanceSales.Remove(saleInDb);
            _context.SaveChanges();

            return Ok("Sale deleted");
        }


        /// <summary>
        /// Gets a list of sales that have not been marked as completed.
        /// </summary>
        /// <returns>Returns a list of DTOs of sales that have not been marked as completed.</returns>
        [HttpGet]
        [Route("api/sales/processed")]
        public IEnumerable<FinanceSaleDto> GetPendingSales()
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return _context.FinanceSales.Where(x => x.Processed && x.AcademicYearId == academicYearId)
                .ToList()
                .Select(Mapper.Map<FinanceSale, FinanceSaleDto>);
        }

        /// <summary>
        /// Gets a list of all sales
        /// </summary>
        /// <returns>Returns a list of DTOs of all sales.</returns>
        [HttpGet]
        [Route("api/sales/all")]
        public IEnumerable<FinanceSaleDto> GetSales()
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return _context.FinanceSales
                .Where(x => x.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<FinanceSale, FinanceSaleDto>);
        }

        /// <summary>
        /// Gets a list of sales for a particular student.
        /// </summary>
        /// <param name="studentId">The ID of the student to fetch sales for.</param>
        /// <returns>Returns a list of DTOs of sales for the specified student</returns>
        [HttpGet]
        [Route("api/sales/student")]
        public IEnumerable<FinanceSaleDto> GetSalesForStudent(int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return _context.FinanceSales
                .Where(x => x.StudentId == studentId && x.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<FinanceSale, FinanceSaleDto>);
        }

        /// <summary>
        /// Gets a list of sales that have not been marked as completed.
        /// </summary>
        /// <returns>Returns a list of DTOs of sales that have not been marked as completed.</returns>
        [HttpGet]
        [Route("api/sales")]
        public IEnumerable<FinanceSaleDto> GetUnprocessedSales()
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return _context.FinanceSales
                .Where(x => x.Processed == false && x.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<FinanceSale, FinanceSaleDto>);
        }

        /// <summary>
        /// Creates a sale for one product.
        /// </summary>
        /// <param name="sale">The sale to create.</param>
        /// <exception cref="HttpResponseException">Thrown if the student or product is not found.</exception>
        public void InvokeSale(FinanceSale sale)
        {            
            var student = _context.Students.SingleOrDefault(x => x.Id == sale.StudentId);

            var product = _context.FinanceProducts.SingleOrDefault(x => x.Id == sale.ProductId);

            if (student == null || product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            /*if (product.Price > student.AccountBalance)
                throw new HttpResponseException(HttpStatusCode.BadRequest);*/

            student.AccountBalance -= product.Price;

            sale.AmountPaid = product.Price;            

            _context.FinanceSales.Add(sale);
        }

        /// <summary>
        /// Marks a sale as processed (completed).
        /// </summary>
        /// <param name="id">The ID of the sale to mark as processed.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/sales/complete/{id}")]
        public IHttpActionResult MarkSaleProcessed(int id)
        {
            var saleInDb = _context.FinanceSales.Single(x => x.Id == id);

            if (saleInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Sale not found");
            }

            if (saleInDb.Processed)
            {
                return Content(HttpStatusCode.BadRequest, "Sale already marked as processed");
            }

            saleInDb.Processed = true;

            _context.SaveChanges();

            return Ok("Sale marked as processed");
        }

        /// <summary>
        /// Creates a sale.
        /// </summary>
        /// <param name="sale">The sale to create.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/sales/new")]
        public IHttpActionResult NewSale(FinanceSale sale)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            sale.Date = DateTime.Now;

            sale.Processed = true;

            var student = _context.Students.SingleOrDefault(x => x.Id == sale.StudentId);

            var product = _context.FinanceProducts.SingleOrDefault(x => x.Id == sale.ProductId);

            if (student == null)
            {
                return Content(HttpStatusCode.NotFound, "Student not found");
            }

            if (product == null)
            {
                return Content(HttpStatusCode.NotFound, "Product not found");
            }

            if (student.FreeSchoolMeals && product.FinanceProductType.IsMeal)
            {
                sale.AmountPaid = 0.00m;
                sale.AcademicYearId = academicYearId;

                _context.FinanceSales.Add(sale);
                _context.SaveChanges();

                return Ok("Sale completed");
            }

            student.AccountBalance -= product.Price;

            sale.AmountPaid = product.Price;
            sale.AcademicYearId = academicYearId;

            _context.FinanceSales.Add(sale);
            _context.SaveChanges();

            return Ok("Sale completed");
        }

        /// <summary>
        /// Processes a purchase made by a student using the online store.
        /// </summary>
        /// <param name="data">The checkout object to process the sale for.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/sales/purchase")]
        public IHttpActionResult Purchase(Checkout data)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            if (User.IsInRole("Student"))
            {
                new StudentsController().AuthenticateStudentRequest(data.StudentId);
            }

            //Check student actually exists
            var student = _context.Students.SingleOrDefault(x => x.Id == data.StudentId);

            if (student == null)
            {
                return Content(HttpStatusCode.NotFound, "Student not found");
            }


            //Obtain items from student's shopping basket
            var basket = _context.FinanceBasketItems.Where(x => x.StudentId == data.StudentId);

            //Check there are actually items in the basket
            if (!basket.Any())
            {
                return Content(HttpStatusCode.BadRequest, "There are no items in your basket");
            }

            //Check student has enough money to afford all items
            var totalCost = basket.Sum(x => x.FinanceProduct.Price);

            if (totalCost > student.AccountBalance)
            {
                return Content(HttpStatusCode.BadRequest, "Insufficient funds");
            }

            //Process sales for each item
            foreach (var item in basket)
            {
                var sale = new FinanceSale
                {
                    StudentId = data.StudentId,
                    ProductId = item.ProductId,
                    Date = DateTime.Today,
                    AcademicYearId = academicYearId
                };

                InvokeSale(sale);
            }

            //Remove items from student's basket once transaction has completed
            _context.FinanceBasketItems.RemoveRange(basket);

            _context.SaveChanges();

            return Ok("Purchase completed");
        }

        /// <summary>
        /// Refunds the specified sale to the student.
        /// </summary>
        /// <param name="id">The ID of the sale to refund.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("api/sales/refund/{id}")]
        public IHttpActionResult RefundSale(int id)
        {
            var saleInDb = _context.FinanceSales.SingleOrDefault(p => p.Id == id);

            if (saleInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Sale not found");
            }

            var amount = saleInDb.AmountPaid;

            var student = saleInDb.CoreStudent;

            student.AccountBalance += amount;

            _context.FinanceSales.Remove(saleInDb);
            _context.SaveChanges();

            return Ok("Sale refunded");
        }
        #endregion
    }
}

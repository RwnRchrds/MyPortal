using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    public class SalesController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public SalesController()
        {
            _context = new MyPortalDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("api/sales")]
        public IEnumerable<SaleDto> GetSales()
        {
            return _context.Sales
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<Sale, SaleDto>);
        }

        //DELETE SALE
        [HttpDelete]
        [Route("api/sales/{id}")]
        public void DeleteSale(int id)
        {
            var saleInDb = _context.Sales.SingleOrDefault(p => p.Id == id);

            if (saleInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Sales.Remove(saleInDb);
            _context.SaveChanges();
        }

        //REFUND SALE
        [HttpDelete]
        [Route("api/sales/refund/{id}")]
        public void RefundSale(int id)
        {
            var saleInDb = _context.Sales.SingleOrDefault(p => p.Id == id);

            if (saleInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var amount = saleInDb.Product1.Price;

            var student = saleInDb.Student1;

            student.AccountBalance += amount;

            _context.Sales.Remove(saleInDb);
            _context.SaveChanges();
        }
    }
}

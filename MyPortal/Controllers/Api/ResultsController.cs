using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    public class ResultsController : ApiController
    {
        private MyPortalDbContext _context;

        public ResultsController()
        {
            _context = new MyPortalDbContext();
        }

        [Route("api/results/new")]
        public void AddResult(ResultDto data)
        {
            var resultInDb = _context.Results.SingleOrDefault(x => x.Student == data.Student && x.Subject == data.Subject && x.ResultSet == data.ResultSet);

            if (resultInDb != null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Results.Add(Mapper.Map<ResultDto, Result>(data));
            _context.SaveChanges();
        }
    }
}

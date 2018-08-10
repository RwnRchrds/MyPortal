using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    public class ResultsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public ResultsController()
        {
            _context = new MyPortalDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpPost]
        [Route("api/results/new")]
        public IHttpActionResult AddResult(ResultDto data)
        {
            var resultInDb = _context.Results.SingleOrDefault(x =>
                x.Student == data.Student && x.Subject == data.Subject && x.ResultSet == data.ResultSet);

            if (resultInDb != null)
                return Content(HttpStatusCode.BadRequest, "Result already exists");

            _context.Results.Add(Mapper.Map<ResultDto, Result>(data));
            _context.SaveChanges();

            return Ok("Result added");
        }

        [HttpGet]
        [Route("api/results/fetch")]
        public IEnumerable<ResultDto> GetResults(int student, int resultset)
        {
            var results = _context.Results
                .Where(r => r.Student == student && r.ResultSet == resultset)
                .ToList()
                .Select(Mapper.Map<Result, ResultDto>);

            return results;
        }
    }
}
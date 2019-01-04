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

        public ResultsController(MyPortalDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        #region Individual Student Results Management
        [HttpPost]
        [Route("api/results/new")]
        public IHttpActionResult AddResult(ResultDto data)
        {
            var resultInDb = _context.Results.SingleOrDefault(x =>
                x.StudentId == data.StudentId && x.SubjectId == data.SubjectId && x.ResultSetId == data.ResultSetId);

            if (resultInDb != null)
                return Content(HttpStatusCode.BadRequest, "Result already exists");

            _context.Results.Add(Mapper.Map<ResultDto, Result>(data));
            _context.SaveChanges();

            return Ok("Result added");
        }

        [HttpGet]
        [Route("api/results/fetch")]
        public IEnumerable<ResultDto> GetResults(int student, int resultSet)
        {
            var results = _context.Results
                .Where(r => r.StudentId == student && r.ResultSetId == resultSet)
                .ToList()
                .Select(Mapper.Map<Result, ResultDto>);

            return results;
        }
        #endregion

        #region Gradebook Results Management

        #endregion
    }
}
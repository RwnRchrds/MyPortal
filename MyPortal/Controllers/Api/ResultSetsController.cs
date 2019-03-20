using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    public class ResultSetsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public ResultSetsController()
        {
            _context = new MyPortalDbContext();
        }

        public ResultSetsController(MyPortalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new result set.
        /// </summary>
        /// <param name="data">The result set to add.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/resultSets/new")]
        public IHttpActionResult CreateResultSet(ResultSet data)
        {
            if (data.Name.IsNullOrWhiteSpace() || !ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid Data");
            }

            var rsToAdd = data;

            var currentRsExists = _context.ResultSets.Any(x => x.IsCurrent) && _context.ResultSets.Any();

            if (!currentRsExists)
            {
                rsToAdd.IsCurrent = true;
            }

            _context.ResultSets.Add(rsToAdd);
            _context.SaveChanges();
            return Ok("Result set created");
        }

        /// <summary>
        /// Deletes the specified result set.
        /// </summary>
        /// <param name="resultSetId">The ID of the result set to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("api/resultSets/delete/{resultSetId}")]
        public IHttpActionResult DeleteResultSet(int resultSetId)
        {
            var resultSet = _context.ResultSets.SingleOrDefault(x => x.Id == resultSetId);

            if (resultSet == null)
            {
                return Content(HttpStatusCode.NotFound, "Result set not found");
            }

            if (resultSet.IsCurrent)
            {
                return Content(HttpStatusCode.BadRequest, "Cannot delete current result set");
            }

            _context.ResultSets.Remove(resultSet);
            _context.SaveChanges();
            return Ok("Result set deleted");
        }

        /// <summary>
        /// Gets the specified result set.
        /// </summary>
        /// <param name="resultSetId">The ID of the specified result set.</param>
        /// <returns>Returns a DTO of the specified result set.</returns>
        /// <exception cref="HttpResponseException">Thrown when the result set is not found.</exception>
        [HttpGet]
        [Route("api/resultSets/byId/{resultSetId}")]
        public ResultSetDto GetResultSet(int resultSetId)
        {
            var resultSet = _context.ResultSets.SingleOrDefault(x => x.Id == resultSetId);

            if (resultSet == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<ResultSet, ResultSetDto>(resultSet);
        }

        /// <summary>
        /// Gets all result sets.
        /// </summary>
        /// <returns>Returns a list of DTOs of all result sets.</returns>
        [HttpGet]
        [Route("api/resultSets/all")]
        public IEnumerable<ResultSetDto> GetResultSets()
        {
            return _context.ResultSets.OrderBy(x => x.Name).ToList().Select(Mapper.Map<ResultSet, ResultSetDto>);
        }

        /// <summary>
        /// Checks if the specified result set contains any results.
        /// </summary>
        /// <param name="id">The ID of the result set to check.</param>
        /// <returns>Returns a boolean value indicating whether the result set contains any results.</returns>
        /// <exception cref="HttpResponseException">Thrown when the result set is not found.</exception>
        [HttpGet]
        [Route("api/resultSets/hasResults/{id}")]
        public bool ResultSetHasResults(int id)
        {
            var resultSet = _context.ResultSets.SingleOrDefault(x => x.Id == id);

            if (resultSet == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return resultSet.Results.Any();
        }


        /// <summary>
        /// Sets the specified result as the current result set.
        /// </summary>
        /// <param name="resultSetId">The ID of the result set to mark as current.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/resultSets/setCurrent/{resultSetId}")]
        public IHttpActionResult SetCurrent(int resultSetId)
        {
            var resultSet = _context.ResultSets.SingleOrDefault(x => x.Id == resultSetId);

            if (resultSet == null)
            {
                return Content(HttpStatusCode.NotFound, "Result set not found");
            }

            if (resultSet.IsCurrent)
            {
                return Content(HttpStatusCode.BadRequest, "Result set is already marked as current");
            }

            var currentCount = _context.ResultSets.Count(x => x.IsCurrent);

            if (currentCount != 1)
            {
                return Content(HttpStatusCode.BadRequest, "Database has lost integrity");
            }

            var currentResultSet = _context.ResultSets.SingleOrDefault(x => x.IsCurrent);

            if (currentResultSet == null)
            {
                return Content(HttpStatusCode.BadRequest, "Could not find current result set");
            }

            currentResultSet.IsCurrent = false;
            resultSet.IsCurrent = true;

            _context.SaveChanges();
            return Ok("Result set marked as current");
        }

        /// <summary>
        /// Updates the specified result set.
        /// </summary>
        /// <param name="data">The result set to update.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/resultSets/update")]
        public IHttpActionResult UpdateResultSet(ResultSet data)
        {
            var resultSet = _context.ResultSets.SingleOrDefault(x => x.Id == data.Id);

            if (resultSet == null)
            {
                return Content(HttpStatusCode.NotFound, "Result set not found");
            }

            resultSet.Name = data.Name;

            _context.SaveChanges();
            return Ok("Result set updated");
        }
    }
}
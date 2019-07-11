using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using MyPortal.Dtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Processes;

namespace MyPortal.Controllers.Api
{
    public class AssessmentController : MyPortalApiController
    {
        /// <summary>
        /// Uploads results from a CSV import file.
        /// </summary>
        /// <param name="resultSetId">The result set to insert results into.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [Route("api/results/import/{resultSetId}")]
        public IHttpActionResult ImportResults(int resultSetId)
        {
            return PrepareResponse(AssessmentProcesses.ImportResultsToResultSet(resultSetId, _context));
        }

        /// <summary>
        /// Adds result to student.
        /// </summary>
        /// <param name="data">Result to add</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/results/create")]
        public IHttpActionResult AddResult(AssessmentResult result)
        {
            return PrepareResponse(AssessmentProcesses.CreateResult(result, _context));
        }

        /// <summary>
        /// Gets results for a student from the specified result set.
        /// </summary>
        /// <param name="studentId">The ID of the student to fetch results for.</param>
        /// <param name="resultSetId">The ID of the result set to fetch results from.</param>
        /// <returns>Returns a list of DTOs of results for a student for the specified result set.</returns>
        [HttpGet]
        [Route("api/results/fetch")]
        public IEnumerable<AssessmentResultDto> GetResults(int studentId, int resultSetId)
        {
            return PrepareResponseObject(AssessmentProcesses.GetResults(studentId, resultSetId, _context));
        }

        [HttpPost]
        [Route("api/resultSets/new")]
        public IHttpActionResult CreateResultSet([FromBody] AssessmentResultSet resultSet)
        {
            return PrepareResponse(AssessmentProcesses.CreateResultSet(resultSet, _context));
        }

        /// <summary>
        /// Deletes the specified result set.
        /// </summary>
        /// <param name="resultSetId">The ID of the result set to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("api/resultSets/delete/{resultSetId:int}")]
        public IHttpActionResult DeleteResultSet([FromUri] int resultSetId)
        {
            return PrepareResponse(AssessmentProcesses.DeleteResultSet(resultSetId, _context));
        }

        /// <summary>
        /// Gets the specified result set.
        /// </summary>
        /// <param name="resultSetId">The ID of the specified result set.</param>
        /// <returns>Returns a DTO of the specified result set.</returns>
        /// <exception cref="HttpResponseException">Thrown when the result set is not found.</exception>
        [HttpGet]
        [Route("api/resultSets/byId/{resultSetId:int}")]
        public AssessmentResultSetDto GetResultSet([FromUri] int resultSetId)
        {
            return PrepareResponseObject(
                AssessmentProcesses.GetResultSet(resultSetId, _context));
        }

        /// <summary>
        /// Gets all result sets.
        /// </summary>
        /// <returns>Returns a list of DTOs of all result sets.</returns>
        [HttpGet]
        [Route("api/resultSets/all")]
        public IEnumerable<AssessmentResultSetDto> GetResultSets()
        {
            return PrepareResponseObject(AssessmentProcesses.GetResultSets(_context));
        }

        /// <summary>
        /// Checks if the specified result set contains any results.
        /// </summary>
        /// <param name="id">The ID of the result set to check.</param>
        /// <returns>Returns a boolean value indicating whether the result set contains any results.</returns>
        /// <exception cref="HttpResponseException">Thrown when the result set is not found.</exception>
        [HttpGet]
        [Route("api/resultSets/hasResults/{id:int}")]
        public bool ResultSetHasResults([FromUri] int id)
        {
            return PrepareResponseObject(AssessmentProcesses.ResultSetContainsResults(id, _context));
        }


        /// <summary>
        /// Sets the specified result as the current result set.
        /// </summary>
        /// <param name="resultSetId">The ID of the result set to mark as current.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/resultSets/setCurrent/{resultSetId:int}")]
        public IHttpActionResult SetCurrent([FromUri] int resultSetId)
        {
            return PrepareResponse(AssessmentProcesses.SetResultSetAsCurrent(resultSetId, _context));
        }

        /// <summary>
        /// Updates the specified result set.
        /// </summary>
        /// <param name="resultSet">The result set to update.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/resultSets/update")]
        public IHttpActionResult UpdateResultSet([FromBody] AssessmentResultSet resultSet)
        {
            return PrepareResponse(AssessmentProcesses.UpdateResultSet(resultSet, _context));
        }
    }
}

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

namespace MyPortal.Controllers.Api
{
    public class AssessmentController : MyPortalApiController
    {
        #region Results
        #region Results Import
        
        /// <summary>
        /// Uploads results from a CSV import file.
        /// </summary>
        /// <param name="resultSetId">The result set to insert results into.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [Route("api/results/import/{resultSetId}")]
        public IHttpActionResult UploadResults(int resultSetId)
        {
            if (!File.Exists(@"C:\MyPortal\Files\Results\import.csv"))
            {
                return Content(HttpStatusCode.NotFound, "File not found");
            }

            var stream = new FileStream(@"C:\MyPortal\Files\Results\import.csv", FileMode.Open);
            var subjects = _context.CurriculumSubjects.OrderBy(x => x.Name).ToList();
            var numResults = 0;
            var resultSet = _context.AssessmentResultSets.SingleOrDefault(x => x.Id == resultSetId);

            if (resultSet == null)
            {
                return Content(HttpStatusCode.NotFound, "Result set not found");
            }

            using (var reader = new StreamReader(stream))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                    {
                        continue;
                    }

                    var values = line.Split(',');
                    for (var i = 0; i < subjects.Count; i++)
                    {
                        var studentMisId = values[4];
                        var student = _context.Students.SingleOrDefault(x => x.MisId == studentMisId);
                        if (student == null)
                        {
                            continue;
                        }

                        var result = new AssessmentResult
                        {
                            StudentId = student.Id,
                            ResultSetId = resultSet.Id,
                            SubjectId = subjects[i].Id,
                            Value = values[5 + i]
                        };

                        if (result.Value.Equals(""))
                        {
                            continue;
                        }

                        _context.AssessmentResults.Add(result);
                        numResults++;
                    }
                }
            }

            stream.Dispose();

            var guid = Guid.NewGuid();
            File.Move(@"C:/MyPortal/Files/Results/import.csv", @"C:/MyPortal/Files/Results/" + guid + "_IMPORTED.csv");

            _context.SaveChanges();
            return Ok(numResults + " results found and imported");
        }

            #endregion               

        #region Individual Student Results Management

        /// <summary>
        /// Adds result to student.
        /// </summary>
        /// <param name="data">Result to add</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/results/create")]
        public IHttpActionResult AddResult(AssessmentResultDto data)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }
            
            var resultInDb = _context.AssessmentResults.SingleOrDefault(x =>
                x.StudentId == data.StudentId && x.SubjectId == data.SubjectId && x.ResultSetId == data.ResultSetId);

            if (resultInDb != null)
            {
                return Content(HttpStatusCode.BadRequest, "Result already exists");
            }

            _context.AssessmentResults.Add(Mapper.Map<AssessmentResultDto, AssessmentResult>(data));
            _context.SaveChanges();

            return Ok("Result added");
        }

        /// <summary>
        /// Gets results for a student from the specified result set.
        /// </summary>
        /// <param name="student">The ID of the student to fetch results for.</param>
        /// <param name="resultSet">The ID of the result set to fetch results from.</param>
        /// <returns>Returns a list of DTOs of results for a student for the specified result set.</returns>
        [HttpGet]
        [Route("api/results/fetch")]
        public IEnumerable<AssessmentResultDto> GetResults(int student, int resultSet)
        {
            var results = _context.AssessmentResults
                .Where(r => r.StudentId == student && r.ResultSetId == resultSet)
                .ToList()
                .Select(Mapper.Map<AssessmentResult, AssessmentResultDto>);

            return results;
        }

        #endregion

        #region Gradebook Results Management

        #endregion
        #endregion

        #region ResultSets
        [HttpPost]
        [Route("api/resultSets/new")]
        public IHttpActionResult CreateResultSet(AssessmentResultSet data)
        {
            if (data.Name.IsNullOrWhiteSpace() || !ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid Data");
            }

            var rsToAdd = data;

            var currentRsExists = _context.AssessmentResultSets.Any(x => x.IsCurrent) && _context.AssessmentResultSets.Any();

            if (!currentRsExists)
            {
                rsToAdd.IsCurrent = true;
            }

            _context.AssessmentResultSets.Add(rsToAdd);
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
            var resultSet = _context.AssessmentResultSets.SingleOrDefault(x => x.Id == resultSetId);

            if (resultSet == null)
            {
                return Content(HttpStatusCode.NotFound, "Result set not found");
            }

            if (resultSet.IsCurrent)
            {
                return Content(HttpStatusCode.BadRequest, "Cannot delete current result set");
            }

            _context.AssessmentResultSets.Remove(resultSet);
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
        public AssessmentResultSetDto GetResultSet(int resultSetId)
        {
            var resultSet = _context.AssessmentResultSets.SingleOrDefault(x => x.Id == resultSetId);

            if (resultSet == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<AssessmentResultSet, AssessmentResultSetDto>(resultSet);
        }

        /// <summary>
        /// Gets all result sets.
        /// </summary>
        /// <returns>Returns a list of DTOs of all result sets.</returns>
        [HttpGet]
        [Route("api/resultSets/all")]
        public IEnumerable<AssessmentResultSetDto> GetResultSets()
        {
            return _context.AssessmentResultSets.OrderBy(x => x.Name).ToList().Select(Mapper.Map<AssessmentResultSet, AssessmentResultSetDto>);
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
            var resultSet = _context.AssessmentResultSets.SingleOrDefault(x => x.Id == id);

            if (resultSet == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return resultSet.AssessmentResults.Any();
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
            var resultSet = _context.AssessmentResultSets.SingleOrDefault(x => x.Id == resultSetId);

            if (resultSet == null)
            {
                return Content(HttpStatusCode.NotFound, "Result set not found");
            }

            if (resultSet.IsCurrent)
            {
                return Content(HttpStatusCode.BadRequest, "Result set is already marked as current");
            }

            var currentCount = _context.AssessmentResultSets.Count(x => x.IsCurrent);

            if (currentCount != 1)
            {
                return Content(HttpStatusCode.BadRequest, "Database has lost integrity");
            }

            var currentResultSet = _context.AssessmentResultSets.SingleOrDefault(x => x.IsCurrent);

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
        public IHttpActionResult UpdateResultSet(AssessmentResultSet data)
        {
            var resultSet = _context.AssessmentResultSets.SingleOrDefault(x => x.Id == data.Id);

            if (resultSet == null)
            {
                return Content(HttpStatusCode.NotFound, "Result set not found");
            }

            resultSet.Name = data.Name;

            _context.SaveChanges();
            return Ok("Result set updated");
        }

        #endregion
    }
}

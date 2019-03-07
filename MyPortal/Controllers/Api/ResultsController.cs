using System;
using System.Collections.Generic;
using System.IO;
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

        [Route("api/results/import/{resultSetId}")]
        public IHttpActionResult UploadResults(int resultSetId)
        {
            if (!File.Exists(@"C:\MyPortal\Files\Results\import.csv"))
            {
                return Content(HttpStatusCode.NotFound, "File not found");
            }

            var stream = new FileStream(@"C:\MyPortal\Files\Results\import.csv", FileMode.Open);
            var subjects = _context.Subjects.OrderBy(x => x.Name).ToList();
            var numResults = 0;
            var resultSet = _context.ResultSets.SingleOrDefault(x => x.Id == resultSetId);

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

                        var result = new Result
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

                        _context.Results.Add(result);
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

        #region Individual Student Results Management

        [HttpPost]
        [Route("api/results/create")]
        public IHttpActionResult AddResult(ResultDto data)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }
            
            var resultInDb = _context.Results.SingleOrDefault(x =>
                x.StudentId == data.StudentId && x.SubjectId == data.SubjectId && x.ResultSetId == data.ResultSetId);

            if (resultInDb != null)
            {
                return Content(HttpStatusCode.BadRequest, "Result already exists");
            }

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    public class LogsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public LogsController()
        {
            _context = new MyPortalDbContext();
        }

        public LogsController(MyPortalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new log to the database.
        /// </summary>
        /// <param name="data">The log to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/logs/new")]
        public IHttpActionResult CreateLog(Log data)
        {
            var authorId = data.AuthorId;

            var author = new Staff();

            if (authorId == 0)
            {
                var userId = User.Identity.GetUserId();
                author = _context.Staff.SingleOrDefault(x => x.UserId == userId);
                if (author == null)
                {
                    return Content(HttpStatusCode.BadRequest, "User does not have a personnel profile");
                }
            }

            if (authorId != 0)
            {
                author = _context.Staff.SingleOrDefault(x => x.Id == authorId);
            }

            if (author == null)
            {
                return Content(HttpStatusCode.NotFound, "Staff member not found");
            }

            data.Date = DateTime.Now;
            data.AuthorId = author.Id;

            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var log = data;
            _context.Logs.Add(log);
            _context.SaveChanges();

            return Ok("Log created");
        }

        /// <summary>
        /// Deletes the specified log from the database.
        /// </summary>
        /// <param name="id">The ID of the log to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [Route("api/logs/log/{id}")]
        public IHttpActionResult DeleteLog(int id)
        {
            var logInDb = _context.Logs.SingleOrDefault(l => l.Id == id);

            if (logInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Log does not exist");
            }

            _context.Logs.Remove(logInDb);
            _context.SaveChanges();

            return Ok("Log deleted");
        }

        /// <summary>
        /// Gets the specified log from the database.
        /// </summary>
        /// <param name="id">The ID of the log to fetch.</param>
        /// <returns>Returns a DTO of the specified log.</returns>
        /// <exception cref="HttpResponseException">Thrown when the log is not found.</exception>
        [HttpGet]
        [Route("api/logs/log/{id}")]
        public LogDto GetLog(int id)
        {
            var log = _context.Logs.SingleOrDefault(l => l.Id == id);

            if (log == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<Log, LogDto>(log);
        }

        /// <summary>
        /// Get all logs for the specified student.
        /// </summary>
        /// <param name="studentId">The ID of the student to fetch logs for.</param>
        /// <returns>Returns a list of DTOs of logs for students.</returns>
        [HttpGet]
        [Route("api/logs/{studentId}")]
        public IEnumerable<LogDto> GetLogs(int studentId)
        {
            return _context.Logs.Where(l => l.StudentId == studentId)
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<Log, LogDto>);
        }

        /// <summary>
        /// Updates the log in the database.
        /// </summary>
        /// <param name="log">The log to be updated in the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [Route("api/logs/log/edit")]
        [HttpPost]
        public IHttpActionResult UpdateLog(Log log)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var logInDb = _context.Logs.SingleOrDefault(l => l.Id == log.Id);

            if (logInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Log not found");
            }

            //var c = Mapper.Map(logDto, logInDb);

            logInDb.TypeId = log.TypeId;
            logInDb.Message = log.Message;

            _context.SaveChanges();

            return Ok("Log updated");
        }
    }
}
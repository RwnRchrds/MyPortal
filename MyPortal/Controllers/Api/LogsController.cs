using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models.Database;
using MyPortal.Processes;

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

        public LogsController(MyPortalDbContext context, IPrincipal user)
        {
            _context = context;
            User = user;
        }

        /// <summary>
        /// Adds a new log to the database.
        /// </summary>
        /// <param name="log">The log to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/logs/new")]
        public IHttpActionResult CreateLog(ProfileLog log)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            var authorId = log.AuthorId;

            var author = new StaffMember();

            if (authorId == 0)
            {
                var userId = User.Identity.GetUserId();
                author = PeopleProcesses.GetStaffFromUserId(userId, _context);
                if (author == null)
                {
                    return Content(HttpStatusCode.BadRequest, "User does not have a personnel profile");
                }
            }

            if (authorId != 0)
            {
                author = _context.StaffMembers.SingleOrDefault(x => x.Id == authorId);
            }

            if (author == null)
            {
                return Content(HttpStatusCode.NotFound, "Staff member not found");
            }

            log.Date = DateTime.Now;
            log.AuthorId = author.Id;
            log.AcademicYearId = academicYearId;

            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            _context.ProfileLogs.Add(log);
            _context.SaveChanges();

            return Ok("Log created");
        }

        /// <summary>
        /// Deletes the specified log from the database.
        /// </summary>
        /// <param name="id">The ID of the log to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [Route("api/logs/log/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteLog(int id)
        {
            var logInDb = _context.ProfileLogs.SingleOrDefault(l => l.Id == id);

            if (logInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Log does not exist");
            }

            _context.ProfileLogs.Remove(logInDb);
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
        public ProfileLogDto GetLog(int id)
        {
            var log = _context.ProfileLogs.SingleOrDefault(l => l.Id == id);

            if (log == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<ProfileLog, ProfileLogDto>(log);
        }

        /// <summary>
        /// Get all logs for the specified student.
        /// </summary>
        /// <param name="studentId">The ID of the student to fetch logs for.</param>
        /// <returns>Returns a list of DTOs of logs for students.</returns>
        [HttpGet]
        [Route("api/logs/{studentId}")]
        public IEnumerable<ProfileLogDto> GetLogs(int studentId)
        {
            if (User.IsInRole("Student"))
            {
                new StudentsController().AuthenticateStudentRequest(studentId);
            }

            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            return _context.ProfileLogs.Where(l => l.StudentId == studentId && l.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<ProfileLog, ProfileLogDto>);
        }

        /// <summary>
        /// Updates the log in the database.
        /// </summary>
        /// <param name="log">The log to be updated in the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [Route("api/logs/log/edit")]
        [HttpPost]
        public IHttpActionResult UpdateLog(ProfileLog log)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var logInDb = _context.ProfileLogs.SingleOrDefault(l => l.Id == log.Id);

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
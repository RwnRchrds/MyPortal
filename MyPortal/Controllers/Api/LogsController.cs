using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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
                if (author == null) return Content(HttpStatusCode.BadRequest, "User does not have a personnel profile");
            }

            if (authorId != 0) author = _context.Staff.SingleOrDefault(x => x.Id == authorId);

            if (author == null) return Content(HttpStatusCode.NotFound, "Staff member not found");

            data.Date = DateTime.Now;
            data.AuthorId = author.Id;

            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, "Invalid data");

            var log = (data);
            _context.Logs.Add(log);
            _context.SaveChanges();

            return Ok("Log created");
        }

        [Route("api/logs/log/{id}")]
        public IHttpActionResult DeleteLog(int id)
        {
            var logInDb = _context.Logs.SingleOrDefault(l => l.Id == id);

            if (logInDb == null)
                return Content(HttpStatusCode.NotFound, "Log does not exist");

            _context.Logs.Remove(logInDb);
            _context.SaveChanges();

            return Ok("Log deleted");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("api/logs/log/{id}")]
        public LogDto GetLog(int id)
        {
            var log = _context.Logs.SingleOrDefault(l => l.Id == id);

            if (log == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Log, LogDto>(log);
        }

        [HttpGet]
        [Route("api/logs/{student}")]
        public IEnumerable<LogDto> GetLogs(int student)
        {
            return _context.Logs.Where(l => l.StudentId == student)
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<Log, LogDto>);
        }

        [Route("api/logs/log/edit")]
        [HttpPost]
        public IHttpActionResult UpdateLog(Log log)
        {
            if (log == null)
                return Content(HttpStatusCode.BadRequest, "No valid data was received");

            var logInDb = _context.Logs.SingleOrDefault(l => l.Id == log.Id);

            if (logInDb == null)
                return Content(HttpStatusCode.NotFound, "Log not found");

            //var c = Mapper.Map(logDto, logInDb);

            logInDb.TypeId = log.TypeId;
            logInDb.Message = log.Message;

            _context.SaveChanges();

            return Ok("Log updated");
        }
    }
}
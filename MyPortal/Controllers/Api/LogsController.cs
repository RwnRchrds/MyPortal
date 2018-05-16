using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    public class LogsController : ApiController
    {
        private MyPortalDbContext _context;

        public LogsController()
        {
            _context = new MyPortalDbContext();
        }

        [Route("api/logs/{student}")]
        public IEnumerable<LogDto> GetLogs(int student)
        {
            return _context.Logs.Where(l => l.Student == student).ToList().Select(Mapper.Map<Log, LogDto>);
        }

        [Route("api/logs/log/{id}")]
        public LogDto GetLog(int id)
        {
            var log = _context.Logs.SingleOrDefault(l => l.Id == id);

            if (log == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Log, LogDto>(log);
        }

        [HttpPost]
        public LogDto CreateLog(LogDto logDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var log = Mapper.Map<LogDto, Log>(logDto);
            _context.Logs.Add(log);
            _context.SaveChanges();

            logDto.Id = log.Id;

            return logDto;
        }

        [Route("api/logs/log/edit")]
        [HttpPost]
        public void UpdateLog(LogDto logDto)
        {
            if (logDto == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var logInDb = _context.Logs.SingleOrDefault(l => l.Id == logDto.Id);

            if (logInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var c = Mapper.Map(logDto, logInDb);
          
            logInDb.Type = logDto.Type;
            logInDb.Message = logDto.Message;

            _context.SaveChanges();
        }

        [Route("api/logs/log/{id}")]
        public void DeleteLog(int id)
        {
            var logInDb = _context.Logs.SingleOrDefault(l => l.Id == id);

            if (logInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Logs.Remove(logInDb);
            _context.SaveChanges();
        }
    }
}

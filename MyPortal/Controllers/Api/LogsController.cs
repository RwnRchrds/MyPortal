using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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

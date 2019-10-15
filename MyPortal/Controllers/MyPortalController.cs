using System;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;

namespace MyPortal.Controllers
{
    public class MyPortalController : Controller
    {
        protected readonly MyPortalDbContext _context;

        public MyPortalController()
        {
            _context = new MyPortalDbContext();
        }

        public MyPortalController(MyPortalDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Obsolete]
        protected T PrepareResponseObject<T>(ProcessResponse<T> response)
        {
            if (response.ResponseType == ResponseType.NotFound)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (response.ResponseType == ResponseType.Ok)
            {
                return response.ResponseObject;
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        protected void ThrowException(Exception ex)
        {
            var statusCode = HttpStatusCode.BadRequest;
            
            if (ex is ProcessException e)
            {
                switch (e.ExceptionType)
                {
                    case ExceptionType.NotFound:
                        statusCode = HttpStatusCode.NotFound;
                        break;
                    case ExceptionType.Forbidden:
                        statusCode = HttpStatusCode.Forbidden;
                        break;
                    case ExceptionType.Conflict:
                        statusCode = HttpStatusCode.Conflict;
                        break;
                }
            }

            throw new HttpResponseException(statusCode);
        }

        protected ActionResult NoAcademicYear()
        {
            return View("~/Views/Error/NoAcademicYear.cshtml");
        }
    }
}
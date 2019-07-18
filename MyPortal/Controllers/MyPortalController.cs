using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MyPortal.Models.Database;
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

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

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
    }
}
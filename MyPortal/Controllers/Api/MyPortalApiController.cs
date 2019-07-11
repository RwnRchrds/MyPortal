using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;
using MyPortal.Processes;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    public class MyPortalApiController : ApiController
    {
        protected readonly MyPortalDbContext _context;

        public MyPortalApiController()
        {
            _context = new MyPortalDbContext();
        }

        public MyPortalApiController(MyPortalDbContext context)
        {
            _context = context;
        }

        public IHttpActionResult PrepareResponse(ProcessResponse<object> response)
        {
            if (response.ResponseType == ResponseType.NotFound)
            {
                return Content(HttpStatusCode.NotFound, response.ResponseMessage);
            }

            if (response.ResponseType == ResponseType.Ok)
            {
                return Ok(response.ResponseMessage);
            }

            return Content(HttpStatusCode.BadRequest, response.ResponseMessage);
        }

        public T PrepareResponseObject<T>(ProcessResponse<T> response)
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

        public IHttpActionResult PrepareDataGridObject<T>(IEnumerable<T> list, DataManagerRequest dm)
        {
            var result = list.PerformDataOperations(dm);

            if (!dm.RequiresCounts) return Json(result);

            return Json(new { result = result.Items, count = result.Count });
        }
    }
}

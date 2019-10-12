using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.AspNet.Identity;
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

        //If ProcessResponse does NOT return an object
        protected IHttpActionResult PrepareResponse(ProcessResponse<object> response)
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

        protected IHttpActionResult HandleException(Exception ex)
        {
            return Content(ex is NotFoundException
                ? HttpStatusCode.NotFound
                : HttpStatusCode.BadRequest, ex.Message);
        }

        protected void ThrowException(Exception ex)
        {
            throw new HttpResponseException(ex is NotFoundException
                ? HttpStatusCode.NotFound
                : HttpStatusCode.BadRequest);
        }

        //If ProcessResponse returns an object
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

        protected IHttpActionResult PrepareDataGridObject<T>(IEnumerable<T> list, DataManagerRequest dm)
        {
            var result = list.PerformDataOperations(dm);

            if (!dm.RequiresCounts) return Json(result);

            return Json(new { result = result.Items, count = result.Count });
        }

        protected void AuthenticateStudentRequest(int studentId)
        {
            if (User.HasPermission("AccessStudentPortal"))
            {
                var userId = User.Identity.GetUserId();
                var studentUser = _context.Students.SingleOrDefault(x => x.Person.UserId == userId);
                var requestedStudent = _context.Students.SingleOrDefault(x => x.Id == studentId);

                if (studentUser == null || requestedStudent == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                if (requestedStudent.Id != studentUser.Id)
                {
                    throw new HttpResponseException(HttpStatusCode.Forbidden);
                }
            }
        }
    }
}

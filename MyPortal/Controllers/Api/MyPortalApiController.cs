using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Models;
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
        [Obsolete]
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

            return Content(statusCode, ex.Message);
        }

        protected HttpResponseException GetException(Exception ex)
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

            return new HttpResponseException(statusCode);
        }

        //If ProcessResponse returns an object
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

        protected IHttpActionResult PrepareDataGridObject<T>(IEnumerable<T> list, DataManagerRequest dm)
        {
            var result = list.PerformDataOperations(dm);

            if (!dm.RequiresCounts) return Json(result);

            return Json(new { result = result.Items, count = result.Count });
        }

        protected async Task AuthenticateStudentRequest(int studentId)
        {
            var userType = await User.GetUserType();
            if (userType == UserType.Student)
            {
                var userId = User.Identity.GetUserId();
                var studentUser = await _context.Students.SingleOrDefaultAsync(x => x.Person.UserId == userId);
                var requestedStudent = await _context.Students.SingleOrDefaultAsync(x => x.Id == studentId);

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

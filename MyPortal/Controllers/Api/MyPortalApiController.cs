using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
            if (User.IsInRole("Student"))
            {
                var userId = User.Identity.GetUserId();
                var studentUser = _context.Students.SingleOrDefault(x => x.Person.UserId == userId);
                var requestedStudent = _context.Students.SingleOrDefault(x => x.Id == studentId);

                if (studentUser == null || requestedStudent == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                if (studentUser.Id != requestedStudent.Id)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
            }
        }
    }
}

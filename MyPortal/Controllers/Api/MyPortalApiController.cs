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
using MyPortal.Exceptions;
using MyPortal.Interfaces;
using MyPortal.Models.Misc;
using MyPortal.Persistence;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    public abstract class MyPortalApiController : ApiController
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected MyPortalApiController()
        {
            UnitOfWork = new UnitOfWork(new MyPortalDbContext());
        }

        protected MyPortalApiController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected override void Dispose(bool disposing)
        {

        }

        protected IHttpActionResult HandleException(Exception ex)
        {
            var statusCode = HttpStatusCode.BadRequest;

            if (ex is ServiceException e)
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

            if (ex is ServiceException e)
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

        protected IHttpActionResult PrepareDataGridObject<T>(IEnumerable<T> list, DataManagerRequest dm)
        {
            var result = list.PerformDataOperations(dm);

            if (!dm.RequiresCounts) return Json(result);

            return Json(new { result = result.Items, count = result.Count });
        }

        protected async Task AuthenticateStudentRequest(int studentId)
        {
            var userType = await User.GetUserTypeAsync();
            if (userType == UserType.Student)
            {
                var userId = User.Identity.GetUserId();
                var studentUser = await UnitOfWork.Students.GetByUserId(userId);
                var requestedStudent = await UnitOfWork.Students.GetByIdAsync(studentId);

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

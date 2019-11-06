﻿using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Areas.Students.ViewModels;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Controllers;
using MyPortal.Models;
using MyPortal.Services;

namespace MyPortal.Areas.Students.Controllers
{
    [RoutePrefix("Finance")]
    [UserType(UserType.Student)]
    public class FinanceController : MyPortalController
    {
        [Route("Store/SalesHistory")]
        public async Task<ActionResult> SalesHistory()
        {
            using (var studentService = new StudentService(UnitOfWork))
            {
                var userId = User.Identity.GetUserId();

                var studentInDb = await studentService.GetStudentFromUserId(userId);

                var viewModel = new StudentSalesHistoryViewModel
                {
                    Student = studentInDb
                };

                return View(viewModel);   
            }
        }

        //Store Page
        [Route("Store/Store")]
        public async Task<ActionResult> Store()
        {
            using (var studentService = new StudentService(UnitOfWork))
            {
                var userId = User.Identity.GetUserId();

                var studentInDb = await studentService.GetStudentFromUserId(userId);

                var viewModel = new StudentStoreViewModel
                {
                    Student = studentInDb
                };

                return View(viewModel);   
            }
        }
    }
}
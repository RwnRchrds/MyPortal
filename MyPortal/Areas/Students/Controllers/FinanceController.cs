using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Areas.Students.ViewModels;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.BusinessLogic.Services;
using MyPortal.Controllers;
using MyPortal.Models;
using MyPortal.Models.Identity;
using MyPortal.Services;

namespace MyPortal.Areas.Students.Controllers
{
    [UserType(UserType.Student)]
    [RouteArea("Students")]
    [RoutePrefix("Finance")]
    public class FinanceController : MyPortalController
    {
        [Route("Store/SalesHistory")]
        public async Task<ActionResult> SalesHistory()
        {
            using (var studentService = new StudentService())
            {
                var userId = User.Identity.GetUserId();

                var studentInDb = await studentService.GetStudentByUserId(userId);

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
            using (var studentService = new StudentService())
            {
                var userId = User.Identity.GetUserId();

                var studentInDb = await studentService.GetStudentByUserId(userId);

                var viewModel = new StudentStoreViewModel
                {
                    Student = studentInDb
                };

                return View(viewModel);   
            }
        }
    }
}
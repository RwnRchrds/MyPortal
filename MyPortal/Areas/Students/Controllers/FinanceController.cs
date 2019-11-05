using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Areas.Students.ViewModels;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Controllers;
using MyPortal.Models;

namespace MyPortal.Areas.Students.Controllers
{
    [RoutePrefix("Students/Finance")]
    [UserType(UserType.Student)]
    public class FinanceController : MyPortalController
    {
        [Route("Store/SalesHistory")]
        public ActionResult SalesHistory()
        {
            var userId = User.Identity.GetUserId();

            var studentInDb = _context.Students.SingleOrDefault(s => s.Person.UserId == userId);

            if (studentInDb == null)
                return HttpNotFound();

            var viewModel = new StudentSalesHistoryViewModel
            {
                Student = studentInDb
            };

            return View("~/Views/Students/Store/SalesHistory.cshtml", viewModel);
        }

        //Store Page
        [Route("Store/Store")]
        public ActionResult Store()
        {
            var userId = User.Identity.GetUserId();

            var studentInDb = _context.Students.SingleOrDefault(s => s.Person.UserId == userId);

            if (studentInDb == null)
                return HttpNotFound();

            var viewModel = new StudentStoreViewModel
            {
                Student = studentInDb
            };

            return View("~/Views/Students/Store/Store.cshtml", viewModel);
        }
    }
}
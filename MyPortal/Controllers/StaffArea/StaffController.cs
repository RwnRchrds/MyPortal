using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Models;
using MyPortal.ViewModels;

namespace MyPortal.Controllers.StaffPortal
{
    [RoutePrefix("Staff/People/Staff")]
    [UserType(UserType.Staff)]
    public class StaffController : MyPortalController
    {
        [Route("People/Staff")]
        [RequiresPermission("ViewStaff")]
        public ActionResult Staff()
        {
            var viewModel = new NewStaffViewModel();
            return View("~/Views/Staff/People/Staff/Staff.cshtml", viewModel);
        }

        [RequiresPermission("ViewStaff")]
        [Route("People/Staff/{id}", Name = "PeopleStaffDetails")]
        public ActionResult StaffDetails(int id)
        {
            var staff = _context.StaffMembers.SingleOrDefault(s => s.Id == id);

            if (staff == null)
                return HttpNotFound();

            var userId = User.Identity.GetUserId();

            var currentStaffId = 0;

            var currentUser = _context.StaffMembers.SingleOrDefault(x => x.Person.UserId == userId);

            if (currentUser != null)
            {
                currentStaffId = currentUser.Id;
            }

            var certificates = _context.PersonnelTrainingCertificates.Where(c => c.StaffId == id).ToList();

            var courses = _context.PersonnelTrainingCourses.ToList();

            var viewModel = new StaffDetailsViewModel
            {
                Staff = staff,
                TrainingCertificates = certificates,
                TrainingCourses = courses,
                CurrentStaffId = currentStaffId
            };

            return View("~/Views/Staff/People/Staff/StaffDetails.cshtml", viewModel);
        }
    }
}
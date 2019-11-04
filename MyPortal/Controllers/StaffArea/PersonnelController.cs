using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Services;

namespace MyPortal.Controllers.StaffPortal
{
    [RoutePrefix("Staff/Personnel")]
    [UserType(UserType.Staff)]
    public class PersonnelController : MyPortalController
    {
        [RequiresPermission("ViewTrainingCourses")]
        [Route("Personnel/TrainingCourses")]
        public ActionResult TrainingCourses()
        {
            return View("~/Views/Staff/Personnel/TrainingCourses.cshtml");
        }

        [RequiresPermission("EditTrainingCourses")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(PersonnelTrainingCourse course)
        {
            if (!ModelState.IsValid) return View("~/Views/Staff/Personnel/NewTrainingCourse.cshtml");

            PersonnelService.CreateCourse(course, _context);

            return RedirectToAction("TrainingCourses", "Portal");
        }

        [RequiresPermission("EditTrainingCourses")]
        [Route("Personnel/TrainingCourses/New")]
        public ActionResult NewCourse()
        {
            return View("~/Views/Staff/Personnel/NewTrainingCourse.cshtml");
        }
    }
}
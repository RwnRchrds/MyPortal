using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Database;

namespace MyPortal.ViewModels
{
    public class ClassEnrolmentsViewModel
    {
        public CurriculumClass Class { get; set; }
        public CurriculumEnrolment Enrolment { get; set; }
    }
}
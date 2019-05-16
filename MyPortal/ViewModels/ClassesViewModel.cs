using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Database;

namespace MyPortal.ViewModels
{
    public class ClassesViewModel
    {
        public CurriculumClass Class { get; set; }
        public IEnumerable<StaffMember> Staff { get; set; }
        public IEnumerable<CurriculumSubject> Subjects { get; set; }   
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Database;

namespace MyPortal.ViewModels
{
    public class SessionsViewModel
    {
        public CurriculumClass Class { get; set; }
        public CurriculumSession Session { get; set; }
        public IEnumerable<AttendancePeriod> Periods { get; set; }  
    }
}
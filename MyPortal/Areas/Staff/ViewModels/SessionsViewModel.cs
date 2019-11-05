using System.Collections.Generic;
using MyPortal.Models.Database;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class SessionsViewModel
    {
        public CurriculumClass Class { get; set; }
        public CurriculumSession Session { get; set; }
        public IEnumerable<AttendancePeriod> Periods { get; set; }  
    }
}
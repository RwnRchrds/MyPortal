using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Database;

namespace MyPortal.ViewModels
{
    public class TakeRegisterViewModel
    {
        public int WeekId { get; set; }
        public AttendancePeriod Period { get; set; }
        public CurriculumClass Class { get; set; }
        public IEnumerable<AttendancePeriod> Periods { get; set; }
    }
}
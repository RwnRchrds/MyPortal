using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Staff.ViewModels.Curriculum
{
    public class SessionsViewModel
    {
        public ClassDto Class { get; set; }
        public SessionDto Session { get; set; }
        public EnrolmentDto Enrolment { get; set; }
        public IEnumerable<PeriodDto> Periods { get; set; }
    }
}
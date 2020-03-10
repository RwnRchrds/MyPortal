using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Details;

namespace MyPortalCore.Areas.Staff.ViewModels.Student
{
    public class StudentOverviewViewModel
    {
        public StudentDetails Student { get; set; }
        public decimal Attendance { get; set; }
        public int ConductPoints { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Logic.Models.DataGrid;
using MyPortal.Logic.Models.Details;

namespace MyPortalCore.Areas.Staff.ViewModels.Student
{
    public class StudentOverviewViewModel
    {
        public StudentDetails Student { get; set; }
        public IEnumerable<DataGridProfileLogNote> LogNotes { get; set; }  
        public decimal Attendance { get; set; }
        public int AchievementPoints { get; set; }
        public int BehaviourPoints { get; set; }
    }   
}

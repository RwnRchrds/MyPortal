using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Logic.Models.Entity;

namespace MyPortalCore.Areas.Staff.ViewModels.Student
{
    public class StudentBehaviourManagementViewModel
    {
        public StudentModel Student { get; set; }
        public IEnumerable<SelectListItem> AchievementTypes { get; set; }
        public IEnumerable<SelectListItem> AchievementOutcomes { get; set; }
        public IEnumerable<SelectListItem> IncidentTypes { get; set; }
        public IEnumerable<SelectListItem> IncidentOutcomes { get; set; }
        public IEnumerable<SelectListItem> IncidentStatus { get; set; }
        public AchievementModel AchievementModel { get; set; }
        public IncidentModel IncidentModel { get; set; }
    }
}
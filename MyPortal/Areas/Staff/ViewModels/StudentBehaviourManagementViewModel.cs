using System.Collections.Generic;
using MyPortal.Models.Database;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class StudentBehaviourManagementViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<BehaviourAchievementType> AchievementTypes { get; set; }
        public IEnumerable<BehaviourIncidentType> BehaviourTypes { get; set; }
        public IEnumerable<SchoolLocation> Locations { get; set; }
        public BehaviourAchievement Achievement { get; set; }
        public BehaviourIncident Incident { get; set; } 
    }
}
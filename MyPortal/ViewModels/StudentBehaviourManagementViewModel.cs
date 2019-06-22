using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Database;

namespace MyPortal.ViewModels
{
    public class StudentBehaviourManagementViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<BehaviourAchievementType> AchievementTypes { get; set; }
        public IEnumerable<BehaviourType> BehaviourTypes { get; set; }
        public IEnumerable<BehaviourLocation> BehaviourLocations { get; set; }
        public BehaviourAchievement Achievement { get; set; }
        public BehaviourIncident Incident { get; set; } 
    }
}
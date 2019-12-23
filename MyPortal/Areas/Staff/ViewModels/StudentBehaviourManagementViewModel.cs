using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class StudentBehaviourManagementViewModel
    {
        public StudentDto Student { get; set; }
        public IEnumerable<AchievementTypeDto> AchievementTypes { get; set; }
        public IEnumerable<IncidentTypeDto> BehaviourTypes { get; set; }
        public IEnumerable<LocationDto> Locations { get; set; }
        public AchievementDto Achievement { get; set; }
        public IncidentDto Incident { get; set; }
    }
}
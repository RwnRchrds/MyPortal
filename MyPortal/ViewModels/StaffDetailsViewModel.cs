using System.Collections.Generic;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.ViewModels
{
    public class StaffDetailsViewModel
    {
        public StaffDetailsViewModel()
        {
            Titles = new List<string> {"Mr", "Miss", "Mrs", "Ms", "Mx", "Prof", "Sir", "Dr", "Lady", "Lord"};
            ObservationOutcomes = new List<string>{"Outstanding","Good","Satisfactory","Inadequate"};
        }

        public CoreStaffMember Staff { get; set; }
        public int CurrentStaffId { get; set; }
        public IEnumerable<PersonnelTrainingCertificate> TrainingCertificates { get; set; }
        public PersonnelTrainingCertificate TrainingCertificate { get; set; }
        public PersonnelObservation PersonnelObservation { get; set; }
        public IEnumerable<string> ObservationOutcomes { get; set; }
        public IEnumerable<PersonnelTrainingCourse> TrainingCourses { get; set; }
        public IEnumerable<PersonnelTrainingStatus> TrainingStatuses { get; set; }
        public IEnumerable<string> Titles { get; set; }
        public CoreStaffDocument Upload { get; set; }
    }
}
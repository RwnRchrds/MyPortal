using System.Collections.Generic;
using MyPortal.Dtos;
using MyPortal.Models;
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

        public Staff Staff { get; set; }
        public int CurrentStaffId { get; set; }
        public IEnumerable<TrainingCertificate> TrainingCertificates { get; set; }
        public TrainingCertificate TrainingCertificate { get; set; }
        public StaffObservation StaffObservation { get; set; }
        public IEnumerable<string> ObservationOutcomes { get; set; }
        public IEnumerable<TrainingCourse> TrainingCourses { get; set; }
        public IEnumerable<TrainingStatus> TrainingStatuses { get; set; }
        public IEnumerable<string> Titles { get; set; }
        public StaffDocument Upload { get; set; }
    }
}
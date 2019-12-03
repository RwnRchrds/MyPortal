using System.Collections.Generic;
using MyPortal.Models.Database;
using MyPortal.Services;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class StaffDetailsViewModel
    {
        public StaffDetailsViewModel()
        {
            var peopleService = new PeopleService();

            Titles = peopleService.GetTitles();
            ObservationOutcomes = new List<string>{"Outstanding","Good","Satisfactory","Inadequate"};

            peopleService.Dispose();
        }

        public StaffMember Staff { get; set; }
        public int CurrentStaffId { get; set; }
        public IEnumerable<PersonnelTrainingCertificate> TrainingCertificates { get; set; }
        public PersonnelTrainingCertificate TrainingCertificate { get; set; }
        public PersonnelObservation PersonnelObservation { get; set; }
        public IEnumerable<string> ObservationOutcomes { get; set; }
        public IEnumerable<PersonnelTrainingCourse> TrainingCourses { get; set; }
        public IEnumerable<string> Titles { get; set; }
        public PersonDocument Upload { get; set; }
    }
}
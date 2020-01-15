using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Services;

namespace MyPortal.Areas.Staff.ViewModels.Staff
{
    public class StaffDetailsViewModel
    {
        public StaffDetailsViewModel()
        {
            var peopleService = new PeopleService();

            Titles = peopleService.GetTitles();
            ObservationOutcomes = new List<string> {"Outstanding", "Good", "Satisfactory", "Inadequate"};

            peopleService.Dispose();
        }

        public StaffMemberDto Staff { get; set; }
        public int CurrentStaffId { get; set; }
        public IEnumerable<TrainingCertificateDto> TrainingCertificates { get; set; }
        public TrainingCertificateDto TrainingCertificate { get; set; }
        public ObservationDto PersonnelObservation { get; set; }
        public IEnumerable<string> ObservationOutcomes { get; set; }
        public IEnumerable<TrainingCourseDto> TrainingCourses { get; set; }
        public IEnumerable<string> Titles { get; set; }
        public PersonAttachmentDto Upload { get; set; }
    }
}
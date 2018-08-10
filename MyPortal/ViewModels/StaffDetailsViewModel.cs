using System.Collections.Generic;
using MyPortal.Models;
using MyPortal.Models.Misc;

namespace MyPortal.ViewModels
{
    public class StaffDetailsViewModel
    {
        public Staff Staff { get; set; }
        public IEnumerable<TrainingCertificate> TrainingCertificates { get; set; }
        public TrainingCertificate TrainingCertificate { get; set; }
        public IEnumerable<TrainingCourse> TrainingCourses { get; set; }
        public IEnumerable<TrainingStatus> TrainingStatuses { get; set; }
        public StaffDocumentUpload Upload { get; set; } 
    }
}
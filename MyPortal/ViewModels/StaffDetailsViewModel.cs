using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models;

namespace MyPortal.ViewModels
{
    public class StaffDetailsViewModel
    {
        public Staff Staff { get; set; }
        public IEnumerable<TrainingCertificate> TrainingCertificates { get; set; }
        public TrainingCertificate TrainingCertificate { get; set; }
        public IEnumerable<TrainingCourse> TrainingCourses { get; set; }
        public IEnumerable<TrainingStatus> TrainingStatuses { get; set; }
    }
}
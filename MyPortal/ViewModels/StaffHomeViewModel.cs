using System.Collections.Generic;
using MyPortal.Models;

namespace MyPortal.ViewModels
{
    public class StaffHomeViewModel
    {
        public Staff CurrentUser { get; set; }
        public IEnumerable<TrainingCertificate> TrainingCertificates { get; set; }
    }
}
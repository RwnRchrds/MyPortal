using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models;

namespace MyPortal.ViewModels
{
    public class StaffHomeViewModel
    {
        public Staff CurrentUser { get; set; }
        public IEnumerable<TrainingCertificate> TrainingCertificates { get; set; }
    }
}
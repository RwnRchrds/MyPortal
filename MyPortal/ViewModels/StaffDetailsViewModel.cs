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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos.GridDtos
{
    public class GridPersonnelTrainingCertificateDto
    {
        public int CourseId { get; set; }
        public int StaffId { get; set; }
        public string CourseCode { get; set; }  
        public string CourseDescription { get; set; }
        public string Status { get; set; }
    }
}
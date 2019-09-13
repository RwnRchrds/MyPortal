using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos.GridDtos
{
    public class GridAssessmentResultDto
    {
        public string StudentName { get; set; }
        public string ResultSet { get; set; }
        public string Subject { get; set; }
        public string Value { get; set; }
    }
}
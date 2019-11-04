using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;

namespace MyPortal.Dtos.GridDtos
{
    public class GridAssessmentResultDto : IGridDto
    {
        public string StudentName { get; set; }
        public string ResultSet { get; set; }
        public string Aspect { get; set; }
        public string Value { get; set; }
    }
}
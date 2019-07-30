using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos.GridDtos
{
    public class GridAssessmentResultSetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCurrent { get; set; }
    }
}
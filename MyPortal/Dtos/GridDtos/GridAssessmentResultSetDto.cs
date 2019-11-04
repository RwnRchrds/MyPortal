using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;

namespace MyPortal.Dtos.GridDtos
{
    public class GridAssessmentResultSetDto : IGridDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCurrent { get; set; }
    }
}
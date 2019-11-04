using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;

namespace MyPortal.Dtos.GridDtos
{
    public class GridCurriculumSessionDto : IGridDto
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string PeriodName { get; set; }
        public string Teacher { get; set; }
        public string Time { get; set; }
    }
}
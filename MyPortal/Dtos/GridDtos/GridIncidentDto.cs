using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos.GridDtos
{
    public class GridIncidentDto
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Location { get; set; }
        public string RecordedBy { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public int Points { get; set; }
        public bool Resolved { get; set; }
    }
}
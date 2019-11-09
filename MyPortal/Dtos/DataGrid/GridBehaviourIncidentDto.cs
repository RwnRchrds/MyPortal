using System;
using MyPortal.Interfaces;

namespace MyPortal.Dtos.DataGrid
{
    public class GridBehaviourIncidentDto : IGridDto
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
using System;
using MyPortal.Interfaces;

namespace MyPortal.Dtos.DataGrid
{
    public class GridPersonnelObservationDto : IGridDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ObserveeName { get; set; }
        public string ObserverName { get; set; }
        public string Outcome { get; set; }
    }
}
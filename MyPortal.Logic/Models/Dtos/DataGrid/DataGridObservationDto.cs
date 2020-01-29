using System;

namespace MyPortal.Logic.Models.Dtos.DataGrid
{
    public class DataGridObservationDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ObserveeName { get; set; }
        public string ObserverName { get; set; }
        public string Outcome { get; set; }

        public string OutcomeColourCode { get; set; }
    }
}
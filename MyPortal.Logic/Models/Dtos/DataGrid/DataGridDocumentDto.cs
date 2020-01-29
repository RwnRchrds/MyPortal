using System;

namespace MyPortal.Logic.Models.Dtos.DataGrid
{
    public class DataGridDocumentDto
    {
        public int DocumentId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool Approved { get; set; }
    }
}
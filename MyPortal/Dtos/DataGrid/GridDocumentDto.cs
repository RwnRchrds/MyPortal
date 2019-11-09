using System;
using MyPortal.Interfaces;

namespace MyPortal.Dtos.DataGrid
{
    public class GridDocumentDto : IGridDto
    {
        public int DocumentId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool Approved { get; set; }
    }
}
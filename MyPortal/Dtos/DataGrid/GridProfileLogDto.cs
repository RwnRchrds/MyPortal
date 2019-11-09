using System;
using MyPortal.Interfaces;

namespace MyPortal.Dtos.DataGrid
{
    public class GridProfileLogDto : IGridDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string AuthorName { get; set; }
        public string LogTypeName { get; set; }
        public string Message { get; set; }
    }
}
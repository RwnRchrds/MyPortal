using System;

namespace MyPortal.Logic.Models.DataGrid
{
    public class DataGridProfileLogNote
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string AuthorName { get; set; }
        public string LogTypeName { get; set; } 
        public string LogTypeColourCode { get; set; }
        public string Message { get; set; }
    }
}
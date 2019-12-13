using System;

namespace MyPortal.BusinessLogic.Dtos.DataGrid
{
    public class DataGridProfileLogDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string AuthorName { get; set; }
        public string LogTypeName { get; set; }
        public string Message { get; set; }
    }
}
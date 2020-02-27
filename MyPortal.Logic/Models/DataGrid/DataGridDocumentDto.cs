using System;

namespace MyPortal.Logic.Models.DataGrid
{
    public class DataGridDocumentDto
    {
        public int DocumentId { get; set; }
        public DateTime UploadedDate { get; set; }
        public string Description { get; set; }
        public string DownloadUrl { get; set; }
        public bool Approved { get; set; }
    }
}
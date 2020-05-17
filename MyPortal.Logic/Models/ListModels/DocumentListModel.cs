using System;

namespace MyPortal.Logic.Models.ListModels
{
    public class DocumentListModel
    {
        public Guid Id { get; set; }
        public DateTime UploadedDate { get; set; }
        public string Description { get; set; }
        public string DownloadUrl { get; set; }
        public bool Approved { get; set; }
    }
}
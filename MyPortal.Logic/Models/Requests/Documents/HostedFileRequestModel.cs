using System;

namespace MyPortal.Logic.Models.Requests.Documents
{
    public class HostedFileRequestModel
    {
        public Guid DocumentId { get; set; }
        public string FileId { get; set; }
    }
}
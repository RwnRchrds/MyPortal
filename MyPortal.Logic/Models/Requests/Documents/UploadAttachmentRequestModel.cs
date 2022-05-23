using System;
using Microsoft.AspNetCore.Http;

namespace MyPortal.Logic.Models.Requests.Documents
{
    public class UploadAttachmentRequestModel
    {
        public Guid DocumentId { get; set; }
        public IFormFile File { get; set; }
    }
}

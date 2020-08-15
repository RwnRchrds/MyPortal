using System;
using Microsoft.AspNetCore.Http;

namespace MyPortal.Logic.Models.Documents
{
    public class UploadAttachmentModel
    {
        public Guid DocumentId { get; set; }
        public IFormFile File { get; set; }
    }
}

using System;
using Microsoft.AspNetCore.Http;

namespace MyPortal.Logic.Models.Requests.Documents;

public class FileUploadRequestModel
{
    public Guid DocumentId { get; set; }
    public IFormFile Attachment { get; set; }
}
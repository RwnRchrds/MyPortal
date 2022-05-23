using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Requests.Documents
{
    public class LinkHostedFileRequestModel
    {
        public Guid DocumentId { get; set; }
        public string FileId { get; set; }
    }
}

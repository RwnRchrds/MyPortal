using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class DocumentDto
    {
        public Guid Id { get; set; }

        public Guid TypeId { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        [Url]
        public string DownloadUrl { get; set; }

        public Guid UploaderId { get; set; }

        public DateTime UploadedDate { get; set; }

        public bool NonPublic { get; set; }

        public bool Approved { get; set; }

        public bool Deleted { get; set; }

        public virtual StaffMemberDto Uploader { get; set; }

        public virtual DocumentTypeDto Type { get; set; }

        public virtual PersonAttachmentDto PersonAttachment { get; set; }

        public virtual HomeworkAttachmentDto HomeworkAttachment { get; set; }
    }
}

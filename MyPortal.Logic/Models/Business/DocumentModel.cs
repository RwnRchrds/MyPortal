using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Business
{
    public class DocumentModel
    {
        public Guid Id { get; set; }

        public Guid TypeId { get; set; }

        public Guid DirectoryId { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        public string FileId { get; set; }

        [Required]
        public string FileName { get; set; }

        public Guid UploaderId { get; set; }

        public DateTime UploadedDate { get; set; }

        public bool NonPublic { get; set; }

        public bool Approved { get; set; }

        public bool Deleted { get; set; }

        public virtual DirectoryModel Directory { get; set; }

        public virtual UserModel Uploader { get; set; }

        public virtual DocumentTypeModel Type { get; set; }
    }
}

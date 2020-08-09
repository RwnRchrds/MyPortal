using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Entity
{
    public class FileModel
    {
        public Guid Id { get; set; }

        public Guid DocumentId { get; set; }

        [Required]
        public string FileId { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string ContentType { get; set; }

        public virtual DocumentModel Document { get; set; }
    }
}

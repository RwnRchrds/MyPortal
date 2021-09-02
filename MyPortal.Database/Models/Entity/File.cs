using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Files")]
    public class File : BaseTypes.Entity
    {
        [Column(Order = 1)]
        [Required]
        public string FileId { get; set; }

        [Column(Order = 2)]
        [Required]
        public string FileName { get; set; }

        [Column(Order = 3)]
        [Required]
        public string ContentType { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}

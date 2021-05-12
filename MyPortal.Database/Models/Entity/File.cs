using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Files")]
    public class File : BaseTypes.Entity
    {
        [Column(Order = 5)]
        [Required]
        public string FileId { get; set; }

        [Column(Order = 6)]
        [Required]
        public string FileName { get; set; }

        [Column(Order = 7)]
        [Required]
        public string ContentType { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}

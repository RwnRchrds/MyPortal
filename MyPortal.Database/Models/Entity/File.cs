using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Files")]
    public class File : BaseTypes.Entity
    {
        [Column(Order = 1)] 
        public Guid DocumentId { get; set; }

        [Column(Order = 5)]
        [Required]
        public string FileId { get; set; }

        [Column(Order = 6)]
        [Required]
        public string FileName { get; set; }

        [Column(Order = 7)]
        [Required]
        public string ContentType { get; set; }

        public virtual Document Document { get; set; }
    }
}

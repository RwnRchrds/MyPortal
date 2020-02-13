using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("Bulletin")]
    public class Bulletin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ExpireDate { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [Required]
        public string Detail { get; set; }

        public bool ShowStudents { get; set; }
        
        public bool Approved { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
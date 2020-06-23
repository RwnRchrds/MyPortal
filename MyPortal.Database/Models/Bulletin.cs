using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("Bulletin")]
    public class Bulletin : IDirectoryEntity
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid DirectoryId { get; set; }

        [Column(Order = 2)]
        public Guid AuthorId { get; set; }

        [Column(Order = 3)]
        public DateTime CreateDate { get; set; }

        [Column(Order = 4)]
        public DateTime? ExpireDate { get; set; }

        [Column(Order = 5)]
        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [Column(Order = 6)]
        [Required]
        public string Detail { get; set; }
            
        [Column(Order = 7)]
        public bool StaffOnly { get; set; }

        [Column(Order = 8)]
        public bool Approved { get; set; }

        public virtual ApplicationUser Author { get; set; }
        public virtual Directory Directory { get; set; }
    }
}
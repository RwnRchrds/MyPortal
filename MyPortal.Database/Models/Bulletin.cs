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
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid DirectoryId { get; set; }

        [DataMember]
        public Guid AuthorId { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public DateTime? ExpireDate { get; set; }

        [DataMember]
        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [DataMember]
        [Required]
        public string Detail { get; set; }
            
        [DataMember]
        public bool StaffOnly { get; set; }

        [DataMember]
        public bool Approved { get; set; }

        public virtual ApplicationUser Author { get; set; }
        public virtual Directory Directory { get; set; }
    }
}
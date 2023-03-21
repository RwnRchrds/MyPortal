using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Bulletins")]
    public class Bulletin : BaseTypes.Entity, IDirectoryEntity, ICreatable
    {
        [Column(Order = 2)]
        public Guid DirectoryId { get; set; }

        [Column(Order = 3)]
        public Guid CreatedById { get; set; }

        [Column(Order = 4)]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 5)]
        public DateTime? ExpireDate { get; set; }

        [Column(Order = 6)]
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Column(Order = 7)]
        [Required]
        public string Detail { get; set; }
            
        [Column(Order = 8)]
        public bool Private { get; set; }

        [Column(Order = 9)]
        public bool Approved { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual Directory Directory { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Documents")]
    public class Document : BaseTypes.Entity, ICreatable, IDirectoryEntity, ISoftDeleteEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Document()
        {
            
        }

        [Column(Order = 1)]
        public Guid TypeId { get; set; }

        [Column(Order = 2)]
        public Guid DirectoryId { get; set; }
        
        [Column(Order = 3)] 
        public Guid? FileId { get; set; }

        [Column(Order = 4)]
        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [Column(Order = 5)]
        [StringLength(256)]
        public string Description { get; set; }

        [Column(Order = 6)]
        public Guid CreatedById { get; set; }

        [Column(Order = 7, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        // Only visible to staff users who have access to the directory
        [Column(Order = 8)]
        public bool Private { get; set; }

        [Column(Order = 10)]
        public bool Deleted { get; set; }

        public virtual User CreatedBy { get; set; }

        public virtual Directory Directory { get; set; }

        public virtual DocumentType Type { get; set; }

        public virtual File Attachment { get; set; }
        
        public virtual ICollection<HomeworkSubmission> HomeworkSubmissions { get; set; }
    }
}

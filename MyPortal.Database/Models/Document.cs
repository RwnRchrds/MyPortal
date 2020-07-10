using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("Document")]
    public partial class Document : ICreationAuditEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Document()
        {
            
        }

        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid TypeId { get; set; }

        [Column(Order = 2)]
        public Guid DirectoryId { get; set; }

        [Column(Order = 3)]
        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [Column(Order = 4)]
        [StringLength(256)]
        public string Description { get; set; }

        [Column(Order = 5)]
        [Required]
        public string FileId { get; set; }

        [Column(Order = 6)]
        [Required]
        public string FileName { get; set; }

        [Column(Order = 7)]
        [Required]
        public string ContentType { get; set; }

        [Column(Order = 8)]
        public Guid CreatedById { get; set; }

        [Column(Order = 9, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 10)]
        public bool Public { get; set; }

        [Column(Order = 11)]
        public bool Approved { get; set; }

        [Column(Order = 12)]
        public bool Deleted { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }

        public virtual Directory Directory { get; set; }

        public virtual HomeworkSubmission HomeworkSubmission { get; set; }

        public virtual DocumentType Type { get; set; }
    }
}

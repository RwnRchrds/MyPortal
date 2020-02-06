using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("Document")]
    public partial class Document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Document()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid TypeId { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        [Url]
        public string DownloadUrl { get; set; } 

        public Guid UploaderId { get; set; }

        [Column(TypeName = "date")]
        public DateTime UploadedDate { get; set; }

        public bool NonPublic { get; set; }

        public bool Approved { get; set; }

        public bool Deleted { get; set; }

        public virtual StaffMember Uploader { get; set; }

        public virtual DocumentType Type { get; set; }

        public virtual PersonAttachment PersonAttachment { get; set; }

        public virtual HomeworkAttachment HomeworkAttachment { get; set; }
    }
}

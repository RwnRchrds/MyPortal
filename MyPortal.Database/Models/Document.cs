using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("Document")]
    public partial class Document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Document()
        {
            
        }

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid TypeId { get; set; }

        [DataMember]
        public Guid DirectoryId { get; set; }

        [DataMember]
        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [DataMember]
        [StringLength(256)]
        public string Description { get; set; }

        [DataMember]
        [Required]
        public string FileId { get; set; }

        [DataMember]
        [Required]
        public string FileName { get; set; }

        [DataMember]
        [Required]
        public string ContentType { get; set; }

        [DataMember]
        public Guid UploaderId { get; set; }

        [DataMember]
        [Column(TypeName = "date")]
        public DateTime UploadedDate { get; set; }

        [DataMember]
        public bool Public { get; set; }

        [DataMember]
        public bool Approved { get; set; }

        [DataMember]
        public bool Deleted { get; set; }

        public virtual ApplicationUser Uploader { get; set; }

        public virtual Directory Directory { get; set; }

        public virtual DocumentType Type { get; set; }
    }
}

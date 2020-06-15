using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("AttendanceCode")]
    public partial class AttendanceCode
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        [Required]
        [StringLength(1)]
        public string Code { get; set; }

        [DataMember]
        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        [DataMember]
        public Guid MeaningId { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public bool Statutory { get; set; }

        public virtual AttendanceCodeMeaning CodeMeaning { get; set; }
    }
}

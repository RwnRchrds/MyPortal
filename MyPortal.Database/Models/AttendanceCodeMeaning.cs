using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("AttendanceCodeMeaning")]
    public class AttendanceCodeMeaning
    {
        public AttendanceCodeMeaning()
        {
            Codes = new HashSet<AttendanceCode>();
        }

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual ICollection<AttendanceCode> Codes { get; set; }
    }
}

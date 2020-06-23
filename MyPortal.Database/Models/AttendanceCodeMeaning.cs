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

        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual ICollection<AttendanceCode> Codes { get; set; }
    }
}

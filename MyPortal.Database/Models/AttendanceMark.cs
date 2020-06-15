using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("AttendanceMark")]
    public class AttendanceMark
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public Guid WeekId { get; set; }

        [DataMember]
        public Guid PeriodId { get; set; }

        [DataMember]
        [Required]
        [StringLength(1)]
        public string Mark { get; set; }

        [DataMember]
        [StringLength(256)]
        public string Comments { get; set; }

        [DataMember]
        public int MinutesLate { get; set; }

        public virtual Period Period { get; set; }

        public virtual Student Student { get; set; }

        public virtual AttendanceWeek Week { get; set; }
    }
}

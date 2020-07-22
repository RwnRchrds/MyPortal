using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("AttendanceMark")]
    public class AttendanceMark : IEntity
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid StudentId { get; set; }

        [Column(Order = 2)]
        public Guid WeekId { get; set; }

        [Column(Order = 3)]
        public Guid PeriodId { get; set; }

        [Column(Order = 4)]
        [Required]
        [StringLength(1)]
        public string Mark { get; set; }

        [Column(Order = 5)]
        [StringLength(256)]
        public string Comments { get; set; }

        [Column(Order = 6)]
        public int MinutesLate { get; set; }

        public virtual AttendancePeriod AttendancePeriod { get; set; }

        public virtual Student Student { get; set; }

        public virtual AttendanceWeek Week { get; set; }
    }
}

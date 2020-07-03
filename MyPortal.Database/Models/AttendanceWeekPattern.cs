using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("AttendanceWeekPattern")]
    public class AttendanceWeekPattern
    {
        public AttendanceWeekPattern()
        {
            AttendanceWeeks = new HashSet<AttendanceWeek>();
            Periods = new HashSet<Period>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Column(Order = 1)] 
        public int Order { get; set; }

        [Column(Order = 2)] 
        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public virtual ICollection<AttendanceWeek> AttendanceWeeks { get; set; }
        public virtual ICollection<Period> Periods { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ExamComponentSittings")]
    public class ExamComponentSitting : Entity
    {
        [Column(Order = 1)]
        public Guid ComponentId { get; set; }

        [Column(Order = 2)]
        public Guid ExamRoomId { get; set; }

        [Column(Order = 4)] 
        public DateTime ExamDate { get; set; }

        [Column(Order = 5)]
        public TimeSpan? ActualStartTime { get; set; }

        [Column(Order = 6)]
        public int ExtraTimePercent { get; set; }

        public virtual ExamComponent Component { get; set; }
        public virtual ExamRoom Room { get; set; }
        public virtual ICollection<ExamSeatAllocation> SeatAllocations { get; set; }
    }
}

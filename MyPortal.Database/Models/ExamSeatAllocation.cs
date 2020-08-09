using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ExamSeatAllocations")]
    public class ExamSeatAllocation : Entity
    {
        [Column(Order = 1)]
        public Guid SittingId { get; set; }

        [Column(Order = 2)]
        public Guid SeatId { get; set; }

        [Column(Order = 3)]
        public Guid CandidateId { get; set; }

        [Column(Order = 4)]
        public bool Active { get; set; }

        [Column(Order = 5)] 
        public bool Attended { get; set; }

        public virtual ExamComponentSitting Sitting { get; set; }
        public virtual ExamRoomSeat Seat { get; set; }
        public virtual ExamCandidate Candidate { get; set; }
    }
}

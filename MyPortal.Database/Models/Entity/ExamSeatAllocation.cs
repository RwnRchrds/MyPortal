using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamSeatAllocations")]
    public class ExamSeatAllocation : BaseTypes.Entity
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

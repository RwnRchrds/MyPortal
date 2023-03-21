using System;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamSeatAllocations")]
    public class ExamSeatAllocation : BaseTypes.Entity, IActivatable
    {
        [Column(Order = 2)]
        public Guid SittingId { get; set; }

        [Column(Order = 3)]
        public int SeatRow { get; set; }
        
        [Column(Order = 4)]
        public int SeatColumn { get; set; }

        [Column(Order = 5)]
        public Guid CandidateId { get; set; }

        [Column(Order = 6)]
        public bool Active { get; set; }

        [Column(Order = 7)] 
        public bool Attended { get; set; }

        public virtual ExamComponentSitting Sitting { get; set; }
        public virtual ExamCandidate Candidate { get; set; }
    }
}

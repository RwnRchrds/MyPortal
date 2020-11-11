using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamSeatAllocationModel : BaseModel
    {
        public Guid SittingId { get; set; }
        
        public Guid SeatId { get; set; }
        
        public Guid CandidateId { get; set; }
        
        public bool Active { get; set; }
        
        public bool Attended { get; set; }

        public virtual ExamComponentSittingModel Sitting { get; set; }
        public virtual ExamRoomSeatModel Seat { get; set; }
        public virtual ExamCandidateModel Candidate { get; set; }
    }
}
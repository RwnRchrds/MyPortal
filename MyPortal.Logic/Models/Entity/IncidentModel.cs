using System;

namespace MyPortal.Logic.Models.Entity
{
    public class IncidentModel
    {
        public Guid Id { get; set; }
        
        public Guid AcademicYearId { get; set; }
        
        public Guid BehaviourTypeId { get; set; }
        
        public Guid StudentId { get; set; }
        
        public Guid LocationId { get; set; }
        
        public Guid RecordedById { get; set; }
        
        public Guid? OutcomeId { get; set; }
        
        public Guid StatusId { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public string Comments { get; set; }
        
        public int Points { get; set; }
        
        public bool Deleted { get; set; }

        public virtual IncidentTypeModel Type { get; set; }

        public virtual LocationModel Location{ get; set; }

        public virtual AcademicYearModel AcademicYear { get; set; }

        public virtual UserModel RecordedBy { get; set; } 

        public virtual StudentModel Student { get; set; }

        public virtual BehaviourOutcomeModel Outcome { get; set; }

        public virtual BehaviourStatusModel Status { get; set; }
    }
}
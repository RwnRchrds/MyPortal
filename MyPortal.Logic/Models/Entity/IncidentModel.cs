using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Models.ListModels;

namespace MyPortal.Logic.Models.Entity
{
    public class IncidentModel
    {
        public Guid Id { get; set; }
        
        public Guid AcademicYearId { get; set; }
        
        [Required(ErrorMessage = "Behaviour type is required.")]
        public Guid BehaviourTypeId { get; set; }
        
        public Guid StudentId { get; set; }
        
        [Required(ErrorMessage = "Location is required.")]
        public Guid LocationId { get; set; }
        
        public Guid RecordedById { get; set; }
        
        public Guid? OutcomeId { get; set; }
        
        [Required(ErrorMessage = "Status is required.")]
        public Guid StatusId { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public string Comments { get; set; }
        
        [Required(ErrorMessage = "Points is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Points cannot be negative.")]
        public int Points { get; set; }
        
        public bool Deleted { get; set; }

        public virtual IncidentTypeModel Type { get; set; }

        public virtual LocationModel Location{ get; set; }

        public virtual AcademicYearModel AcademicYear { get; set; }

        public virtual UserModel RecordedBy { get; set; } 

        public virtual StudentModel Student { get; set; }

        public virtual BehaviourOutcomeModel Outcome { get; set; }

        public virtual BehaviourStatusModel Status { get; set; }

        public IncidentListModel ToListModel()
        {
            return new IncidentListModel(this);
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Scaffolding;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Summary;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class IncidentModel : BaseModel, ILoadable
    {
        public IncidentModel(Incident model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Incident model)
        {
            AcademicYearId = model.AcademicYearId;
            BehaviourTypeId = model.BehaviourTypeId;
            StudentId = model.StudentId;
            LocationId = model.LocationId;
            CreatedById = model.CreatedById;
            OutcomeId = model.OutcomeId;
            StatusId = model.StatusId;
            CreatedDate = model.CreatedDate;
            Comments = model.Comments;
            Points = model.Points;
            Deleted = model.Deleted;

            if (model.Type != null)
            {
                Type = new IncidentTypeModel(model.Type);
            }

            if (model.Location != null)
            {
                Location = new LocationModel(model.Location);
            }

            if (model.AcademicYear != null)
            {
                AcademicYear = new AcademicYearModel(model.AcademicYear);
            }

            if (model.CreatedBy != null)
            {
                CreatedBy = new UserModel(model.CreatedBy);
            }

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }

            if (model.Outcome != null)
            {
                Outcome = new BehaviourOutcomeModel(model.Outcome);
            }

            if (model.Status != null)
            {
                Status = new BehaviourStatusModel(model.Status);
            }
        }
        
        public Guid AcademicYearId { get; set; }
        
        [Required(ErrorMessage = "Behaviour type is required.")]
        public Guid BehaviourTypeId { get; set; }
        
        public Guid StudentId { get; set; }
        
        public Guid? LocationId { get; set; }
        
        public Guid CreatedById { get; set; }
        
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

        public virtual UserModel CreatedBy { get; set; } 

        public virtual StudentModel Student { get; set; }

        public virtual BehaviourOutcomeModel Outcome { get; set; }

        public virtual BehaviourStatusModel Status { get; set; }

        public IncidentSummaryModel ToListModel()
        {
            return new IncidentSummaryModel(this);
        }

        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Incidents.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Curriculum;
using MyPortal.Logic.Models.Data.School;
using MyPortal.Logic.Models.Data.Settings;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Behaviour.Incidents
{
    public class IncidentModel : BaseModelWithLoad
    {
        public IncidentModel(Incident model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Incident model)
        {
            AcademicYearId = model.AcademicYearId;
            BehaviourTypeId = model.BehaviourTypeId;
            LocationId = model.LocationId;
            CreatedById = model.CreatedById;
            CreatedDate = model.CreatedDate;
            Comments = model.Comments;
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
        }
        
        public Guid AcademicYearId { get; set; }
        
        [Required(ErrorMessage = "Behaviour type is required.")]
        public Guid BehaviourTypeId { get; set; }

        public Guid? LocationId { get; set; }
        
        public Guid CreatedById { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public string Comments { get; set; }

        public bool Deleted { get; set; }

        public virtual IncidentTypeModel Type { get; set; }

        public virtual LocationModel Location{ get; set; }

        public virtual AcademicYearModel AcademicYear { get; set; }

        public virtual UserModel CreatedBy { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Incidents.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
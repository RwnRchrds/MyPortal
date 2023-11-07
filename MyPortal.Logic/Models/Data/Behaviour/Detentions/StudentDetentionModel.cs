using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Students;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Behaviour.Detentions
{
    public class StudentDetentionModel : BaseModelWithLoad
    {
        public StudentDetentionModel(StudentDetention model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(StudentDetention model)
        {
            StudentId = model.StudentId;
            LinkedIncidentId = model.LinkedIncidentId;
            DetentionId = model.DetentionId;

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }

            if (model.Detention != null)
            {
                Detention = new DetentionModel(model.Detention);
            }

            if (model.LinkedIncident != null)
            {
                LinkedIncident = new StudentIncidentModel(model.LinkedIncident);
            }
        }

        public Guid StudentId { get; set; }

        public Guid DetentionId { get; set; }

        public Guid? LinkedIncidentId { get; set; }

        public virtual StudentModel Student { get; set; }
        public virtual StudentIncidentModel LinkedIncident { get; set; }
        public virtual DetentionModel Detention { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.StudentDetentions.GetById(Id.Value);

                LoadFromModel(model);
            }
        }
    }
}
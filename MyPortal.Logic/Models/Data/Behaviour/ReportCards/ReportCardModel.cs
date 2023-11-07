using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Behaviour.Incidents;
using MyPortal.Logic.Models.Data.Students;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Behaviour.ReportCards
{
    public class ReportCardModel : BaseModelWithLoad
    {
        public ReportCardModel(ReportCard model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ReportCard model)
        {
            StudentId = model.StudentId;
            BehaviourTypeId = model.BehaviourTypeId;
            StartDate = model.StartDate;
            EndDate = model.EndDate;
            Comments = model.Comments;
            Active = model.Active;

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }

            if (model.BehaviourType != null)
            {
                BehaviourType = new IncidentTypeModel(model.BehaviourType);
            }
        }

        public Guid StudentId { get; set; }

        public Guid BehaviourTypeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [StringLength(256)] public string Comments { get; set; }

        public bool Active { get; set; }

        public virtual StudentModel Student { get; set; }
        public virtual IncidentTypeModel BehaviourType { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ReportCards.GetById(Id.Value);

                LoadFromModel(model);
            }
        }
    }
}
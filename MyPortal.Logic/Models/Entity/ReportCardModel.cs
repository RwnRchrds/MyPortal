using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ReportCardModel : BaseModel, ILoadable
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

        [StringLength(256)]
        public string Comments { get; set; }

        public bool Active { get; set; }

        public virtual StudentModel Student { get; set; }
        public virtual IncidentTypeModel BehaviourType { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.ReportCards.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}

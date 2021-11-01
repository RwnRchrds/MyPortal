using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamAwardElementModel : BaseModel, ILoadable
    {
        public ExamAwardElementModel(ExamAwardElement model) : base(model)
        {
            
        }

        private void LoadFromModel(ExamAwardElement model)
        {
            AwardId = model.AwardId;
            ElementId = model.ElementId;

            if (model.Award != null)
            {
                Award = new ExamAwardModel(model.Award);
            }

            if (model.Element != null)
            {
                Element = new ExamElementModel(model.Element);
            }
        }
        
        public Guid AwardId { get; set; }
        
        public Guid ElementId { get; set; }

        public virtual ExamAwardModel Award { get; set; }
        public virtual ExamElementModel Element { get; set; }
        
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamAwardElements.GetById(Id.Value);
                LoadFromModel(model);
            }
        }
    }
}
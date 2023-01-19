using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamElementComponentModel : BaseModelWithLoad
    {
        public ExamElementComponentModel(ExamElementComponent model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamElementComponent model)
        {
            ElementId = model.ElementId;
            ComponentId = model.ComponentId;

            if (model.Element != null)
            {
                Element = new ExamElementModel(model.Element);
            }

            if (model.Component != null)
            {
                Component = new ExamComponentModel(model.Component);
            }
        }
        
        public Guid ElementId { get; set; }
        public Guid ComponentId { get; set; }

        public virtual ExamElementModel Element { get; set; }
        public virtual ExamComponentModel Component { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamElementComponents.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
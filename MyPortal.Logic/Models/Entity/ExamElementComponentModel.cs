using System;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamElementComponentModel : BaseModel
    {
        public ExamElementComponentModel(ExamElementComponent model)
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
    }
}
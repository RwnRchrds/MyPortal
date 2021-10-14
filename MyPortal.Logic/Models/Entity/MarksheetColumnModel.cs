using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class MarksheetColumnModel : BaseModel, ILoadable
    {
        public MarksheetColumnModel(MarksheetColumn model) : base(model)
        {
           LoadFromModel(model);
        }

        private void LoadFromModel(MarksheetColumn model)
        {
            TemplateId = model.TemplateId;
            AspectId = model.AspectId;
            ResultSetId = model.ResultSetId;
            DisplayOrder = model.DisplayOrder;
            ReadOnly = model.ReadOnly;

            if (model.Template != null)
            {
                Template = new MarksheetTemplateModel(model.Template);
            }

            if (model.Aspect != null)
            {
                Aspect = new AspectModel(model.Aspect);
            }

            if (model.ResultSet != null)
            {
                ResultSet = new ResultSetModel(model.ResultSet);
            }
        }
        
        public Guid TemplateId { get; set; }
        public Guid AspectId { get; set; }
        public Guid ResultSetId { get; set; }
        public int DisplayOrder { get; set; }
        public bool ReadOnly { get; set; }

        public virtual MarksheetTemplateModel Template { get; set; }
        public virtual AspectModel Aspect { get; set; }
        public virtual ResultSetModel ResultSet { get; set; }
        
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.MarksheetColumns.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}

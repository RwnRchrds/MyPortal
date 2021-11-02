using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Scaffolding;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class MarksheetTemplateGroupModel : BaseModel, ILoadable
    {
        public MarksheetTemplateGroupModel(MarksheetTemplateGroup model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(MarksheetTemplateGroup model)
        {
            MarksheetTemplateId = model.MarksheetTemplateId;
            StudentGroupId = model.StudentGroupId;
            Completed = model.Completed;

            if (model.Template != null)
            {
                Template = new MarksheetTemplateModel(model.Template);
            }

            if (model.StudentGroup != null)
            {
                StudentGroup = new StudentGroupModel(model.StudentGroup);
            }
        }

        public bool Completed { get; set; } 

        public Guid MarksheetTemplateId { get; set; }
        public Guid StudentGroupId { get; set; }

        public virtual MarksheetTemplateModel Template { get; set; }
        public virtual StudentGroupModel StudentGroup { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.MarksheetTemplateGroups.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

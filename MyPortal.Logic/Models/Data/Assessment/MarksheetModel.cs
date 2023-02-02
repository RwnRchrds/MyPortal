using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Curriculum;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Assessment
{
    public class MarksheetModel : BaseModelWithLoad
    {
        public MarksheetModel(Marksheet model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Marksheet model)
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

        public string Name => $"{Template.Name} ({StudentGroup.Code})";

        public bool Completed { get; set; } 

        public Guid MarksheetTemplateId { get; set; }
        public Guid StudentGroupId { get; set; }

        public virtual MarksheetTemplateModel Template { get; set; }
        public virtual StudentGroupModel StudentGroup { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Marksheets.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

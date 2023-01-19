using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class HomeworkItemModel : BaseModelWithLoad
    {
        public HomeworkItemModel(HomeworkItem model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(HomeworkItem model)
        {
            DirectoryId = model.DirectoryId;
            Title = model.Title;
            Description = model.Description;
            SubmitOnline = model.SubmitOnline;
            MaxPoints = model.MaxPoints;

            if (model.Directory != null)
            {
                Directory = new DirectoryModel(model.Directory);
            }
        }

        public int MaxPoints { get; set; }  

        public Guid DirectoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool SubmitOnline { get; set; }

        public virtual DirectoryModel Directory { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.HomeworkItems.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

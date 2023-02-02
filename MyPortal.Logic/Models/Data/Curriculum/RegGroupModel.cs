using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Curriculum
{
    public class RegGroupModel : BaseModelWithLoad
    {
        public RegGroupModel(RegGroup model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(RegGroup model)
        {
            StudentGroupId = model.StudentGroupId;
            YearGroupId = model.YearGroupId;

            if (model.StudentGroup != null)
            {
                StudentGroup = new StudentGroupModel(model.StudentGroup);
            }

            if (model.YearGroup != null)
            {
                YearGroup = new YearGroupModel(model.YearGroup);
            }
        }

        public Guid StudentGroupId { get; set; }
        
        public Guid YearGroupId { get; set; }

        public virtual StudentGroupModel StudentGroup { get; set; }

        public virtual YearGroupModel YearGroup { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.RegGroups.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
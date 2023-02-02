using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Curriculum
{
    public class YearGroupModel : BaseModelWithLoad
    {
        public YearGroupModel(YearGroup model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(YearGroup model)
        {
            StudentGroupId = model.StudentGroupId;
            CurriculumYearGroupId = model.CurriculumYearGroupId;

            if (model.StudentGroup != null)
            {
                StudentGroup = new StudentGroupModel(model.StudentGroup);
            }

            if (model.CurriculumYearGroup != null)
            {
                CurriculumYearGroup = new CurriculumYearGroupModel(model.CurriculumYearGroup);
            }
        }
        
        public Guid StudentGroupId { get; set; }
        
        public Guid CurriculumYearGroupId { get; set; }

        public virtual StudentGroupModel StudentGroup { get; set; }
        public virtual CurriculumYearGroupModel CurriculumYearGroup { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.YearGroups.GetById(Id.Value);
                
                LoadFromModel(model);
            }
        }
    }
}
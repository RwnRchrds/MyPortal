using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Curriculum
{
    public class CurriculumGroupModel : BaseModelWithLoad
    {
        public CurriculumGroupModel(CurriculumGroup model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(CurriculumGroup model)
        {
            BlockId = model.BlockId;
            StudentGroupId = model.StudentGroupId;

            if (model.Block != null)
            {
                Block = new CurriculumBlockModel(model.Block);
            }

            if (model.StudentGroup != null)
            {
                StudentGroup = new StudentGroupModel(model.StudentGroup);
            }
        }

        public Guid BlockId { get; set; }
        public Guid StudentGroupId { get; set; }

        public CurriculumBlockModel Block { get; set; }
        public StudentGroupModel StudentGroup { get; set; }
        
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.CurriculumGroups.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Curriculum
{
    public class CurriculumBandBlockAssignmentModel : BaseModelWithLoad
    {
        public CurriculumBandBlockAssignmentModel(CurriculumBandBlockAssignment model) : base(model)
        {
            
        }

        private void LoadFromModel(CurriculumBandBlockAssignment model)
        {
            BlockId = model.BlockId;
            BandId = model.BandId;

            if (model.Block != null)
            {
                Block = new CurriculumBlockModel(model.Block);
            }

            if (model.Band != null)
            {
                Band = new CurriculumBandModel(model.Band);
            }
        }
        
        public Guid BlockId { get; set; }
        
        public Guid BandId { get; set; }
        
        public CurriculumBlockModel Block { get; set; }
        public CurriculumBandModel Band { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var assignment = await unitOfWork.CurriculumBandBlockAssignments.GetById(Id.Value);

                if (assignment != null)
                {
                    LoadFromModel(assignment);
                }
            }
        }
    }
}
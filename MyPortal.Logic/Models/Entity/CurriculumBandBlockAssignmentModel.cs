using System;
using Microsoft.EntityFrameworkCore.Scaffolding;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class CurriculumBandBlockAssignmentModel : BaseModel, ILoadable
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
        
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.CurriculumBandBlockAssignments.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
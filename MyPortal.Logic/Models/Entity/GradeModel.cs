using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class GradeModel : BaseModel, ILoadable
    {
        public GradeModel(Grade model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Grade model)
        {
            GradeSetId = model.GradeSetId;
            Code = model.Code;
            Description = model.Description;
            Value = model.Value;

            if (model.GradeSet != null)
            {
                GradeSet = new GradeSetModel(model.GradeSet);
            }
        }
        
        public Guid GradeSetId { get; set; }
        
        [Required]
        [StringLength(25)]
        public string Code { get; set; }
        
        [StringLength(50)]
        public string Description { get; set; }
        
        public decimal Value { get; set; }

        public virtual GradeSetModel GradeSet { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.Grades.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}

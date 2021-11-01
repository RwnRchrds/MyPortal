using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ClassModel : BaseModel, ILoadable
    {
        public ClassModel(Class model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Class model)
        {
            CourseId = model.CourseId;
            CurriculumGroupId = model.CurriculumGroupId;
            Code = model.Code;

            if (model.Course != null)
            {
                Course = new CourseModel(model.Course);
            }

            if (model.Group != null)
            {
                Group = new CurriculumGroupModel(model.Group);
            }
        }
        
        public Guid CourseId { get; set; }

        public Guid CurriculumGroupId { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        public CourseModel Course { get; set; }
        public CurriculumGroupModel Group { get; set; }
        
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Classes.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

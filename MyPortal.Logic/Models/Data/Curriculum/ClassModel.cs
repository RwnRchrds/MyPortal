using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Documents;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Curriculum
{
    public class ClassModel : BaseModelWithLoad
    {
        public ClassModel(Class model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Class model)
        {
            CourseId = model.CourseId;
            CurriculumGroupId = model.CurriculumGroupId;
            DirectoryId = model.DirectoryId;
            Code = model.Code;

            if (model.Course != null)
            {
                Course = new CourseModel(model.Course);
            }

            if (model.Group != null)
            {
                Group = new CurriculumGroupModel(model.Group);
            }

            if (model.Directory != null)
            {
                Directory = new DirectoryModel(model.Directory);
            }
        }
        
        public Guid CourseId { get; set; }

        public Guid CurriculumGroupId { get; set; }

        public Guid DirectoryId { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        public DirectoryModel Directory { get; set; }
        public CourseModel Course { get; set; }
        public CurriculumGroupModel Group { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var currClass = await unitOfWork.Classes.GetById(Id.Value);

                if (currClass != null)
                {
                    LoadFromModel(currClass);
                }
            }
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Curriculum
{
    public class SubjectModel : BaseModelWithLoad
    {
        public SubjectModel(Subject model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Subject model)
        {
            SubjectCodeId = model.SubjectCodeId;
            Name = model.Name;
            Code = model.Code;
            Deleted = model.Deleted;

            if (model.SubjectCode != null)
            {
                SubjectCode = new SubjectCodeModel(model.SubjectCode);
            }
        }

        public Guid SubjectCodeId { get; set; }

        [Required] [StringLength(256)] public string Name { get; set; }

        [Required] [StringLength(5)] public string Code { get; set; }

        public bool Deleted { get; set; }

        public virtual SubjectCodeModel SubjectCode { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Subjects.GetById(Id.Value);

                LoadFromModel(model);
            }
        }
    }
}
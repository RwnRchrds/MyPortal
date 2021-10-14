using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class MedicalEventModel : BaseModel, ILoadable
    {
        public MedicalEventModel(MedicalEvent model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(MedicalEvent model)
        {
            StudentId = model.StudentId;
            CreatedById = model.CreatedById;
            Date = model.Date;
            Note = model.Note;

            if (model.CreatedBy != null)
            {
                CreatedBy = new UserModel(model.CreatedBy);
            }

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }
        }
        
        public Guid StudentId { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Note { get; set; }

        public virtual UserModel CreatedBy { get; set; }

        public virtual StudentModel Student { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.MedicalEvents.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}

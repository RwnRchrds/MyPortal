using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.People;
using MyPortal.Logic.Models.Data.Settings;
using MyPortal.Logic.Models.Data.Students;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Medical
{
    public class MedicalEventModel : BaseModelWithLoad
    {
        public MedicalEventModel(MedicalEvent model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(MedicalEvent model)
        {
            StudentId = model.PersonId;
            CreatedById = model.CreatedById;
            Date = model.Date;
            Note = model.Note;

            if (model.CreatedBy != null)
            {
                CreatedBy = new UserModel(model.CreatedBy);
            }

            if (model.Person != null)
            {
                Person = new PersonModel(model.Person);
            }
        }
        
        public Guid StudentId { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Note { get; set; }

        public virtual UserModel CreatedBy { get; set; }

        public virtual PersonModel Person { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.MedicalEvents.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

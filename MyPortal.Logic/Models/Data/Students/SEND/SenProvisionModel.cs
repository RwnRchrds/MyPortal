using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Students.SEND
{
    public class SenProvisionModel : BaseModelWithLoad
    {
        public SenProvisionModel(SenProvision model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(SenProvision model)
        {
            StudentId = model.StudentId;
            ProvisionTypeId = model.ProvisionTypeId;
            StartDate = model.StartDate;
            EndDate = model.EndDate;
            Note = model.Note;

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }

            if (model.Type != null)
            {
                Type = new SenProvisionTypeModel(model.Type);
            }
        }
        
        public Guid StudentId { get; set; }
        
        public Guid ProvisionTypeId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        [Required]
        public string Note { get; set; }

        public virtual StudentModel Student { get; set; }

        public virtual SenProvisionTypeModel Type { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.SenProvisions.GetById(Id.Value);
                LoadFromModel(model);
            }
        }
    }
}
using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class BillModel : BaseModel, ILoadable
    {
        public BillModel(Bill model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Bill model)
        {
            StudentId = model.StudentId;
            CreatedDate = model.CreatedDate;
            DueDate = model.DueDate;
            Dispatched = model.Dispatched;

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }
        }
        
        public Guid StudentId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DueDate { get; set; }

        public bool? Dispatched  { get; set; }

        public StudentModel Student { get; set; }
        
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.Bills.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}

using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Students;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Finance
{
    public class BillModel : BaseModelWithLoad
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

        public bool? Dispatched { get; set; }

        public StudentModel Student { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var bill = await unitOfWork.Bills.GetById(Id.Value);

                if (bill != null)
                {
                    LoadFromModel(bill);
                }
            }
        }
    }
}
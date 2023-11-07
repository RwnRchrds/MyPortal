using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Students;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Finance
{
    public class BasketItemModel : BaseModelWithLoad
    {
        public BasketItemModel(BasketItem model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(BasketItem model)
        {
            StudentId = model.StudentId;
            ProductId = model.ProductId;
            Quantity = model.Quantity;

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }

            if (model.Product != null)
            {
                Product = new ProductModel(model.Product);
            }
        }

        public Guid StudentId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public virtual StudentModel Student { get; set; }

        public virtual ProductModel Product { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var item = await unitOfWork.BasketItems.GetById(Id.Value);

                if (item != null)
                {
                    LoadFromModel(item);
                }
            }
        }
    }
}
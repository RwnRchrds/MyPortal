using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class BasketItemModel : BaseModel, ILoadable
    {
        public BasketItemModel(BasketItem model) : base(model)
        {
           LoadFromModel(model);
        }

        private void LoadFromModel(BasketItem model)
        {
            StudentId = model.StudentId;
            ProductId = model.ProductId;

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

        public virtual StudentModel Student { get; set; }

        public virtual ProductModel Product { get; set; }
        
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.BasketItems.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}

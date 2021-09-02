using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class BillItemModel : BaseModel, ILoadable
    {
        public BillItemModel(BillItem model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(BillItem model)
        {
            BillId = model.BillId;
            ProductId = model.ProductId;
            Quantity = model.Quantity;
            GrossAmount = model.GrossAmount;
            CustomerReceived = model.CustomerReceived;
            Refunded = model.Refunded;

            if (model.Bill != null)
            {
                Bill = new BillModel(model.Bill);
            }

            if (model.Product != null)
            {
                Product = new ProductModel(model.Product);
            }
        }
        
        public Guid BillId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal GrossAmount { get; set; }

        public bool CustomerReceived { get; set; }

        public bool Refunded { get; set; }

        public virtual BillModel Bill { get; set; }
        public virtual ProductModel Product { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.BillItems.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}

using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Finance
{
    public class BillItemModel : BaseModelWithLoad
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
            NetAmount = model.NetAmount;
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

        public decimal NetAmount { get; set; }

        public decimal VatAmount { get; set; }

        public decimal GrossAmount => NetAmount + VatAmount;

        public bool CustomerReceived { get; set; }

        public bool Refunded { get; set; }

        public virtual BillModel Bill { get; set; }
        public virtual ProductModel Product { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var billItem = await unitOfWork.BillItems.GetById(Id.Value);

                if (billItem != null)
                {
                    LoadFromModel(billItem);
                }
            }
        }
    }
}
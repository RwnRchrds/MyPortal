using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class VatRateModel : LookupItemModel
    {
        public VatRateModel(VatRate model) : base(model)
        {
            Value = model.Value;
        }
        
        public decimal Value { get; set; }
    }
}
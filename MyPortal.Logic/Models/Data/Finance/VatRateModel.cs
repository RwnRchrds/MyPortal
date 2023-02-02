using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Finance
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
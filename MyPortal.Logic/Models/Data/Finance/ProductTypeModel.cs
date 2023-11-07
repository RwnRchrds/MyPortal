using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Finance
{
    public class ProductTypeModel : LookupItemModel
    {
        public ProductTypeModel(ProductType model) : base(model)
        {
            IsMeal = model.IsMeal;
        }

        public bool IsMeal { get; set; }
    }
}
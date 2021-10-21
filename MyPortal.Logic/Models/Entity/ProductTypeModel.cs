using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
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

using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class BasketItemModel : BaseModel
    {
        public Guid StudentId { get; set; }

        public Guid ProductId { get; set; }

        public virtual StudentModel Student { get; set; }

        public virtual ProductModel Product { get; set; }
    }
}

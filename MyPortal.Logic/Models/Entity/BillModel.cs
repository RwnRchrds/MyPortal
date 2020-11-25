using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Constants;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class BillModel : BaseModel
    {
        public Guid StudentId { get; set; }

        public DateTime Date { get; set; }

        public decimal NetAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public bool Refunded { get; set; }

        public bool Deleted { get; set; }

        public virtual StudentModel Student { get; set; }
    }
}

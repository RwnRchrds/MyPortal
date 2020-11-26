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

        public DateTime CreatedDate { get; set; }

        public DateTime DueDate { get; set; }

        public bool? Dispatched  { get; set; }

        public virtual StudentModel Student { get; set; }
    }
}

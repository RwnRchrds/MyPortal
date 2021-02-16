using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace MyPortal.Database.Models.Entity
{
    public class ParentEveningGroup : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid ParentEveningId { get; set; }

        [Column(Order = 2)]
        public Guid GroupTypeId { get; set; }

        [Column(Order = 3)]
        public Guid GroupId { get; set; }

        public virtual ParentEvening ParentEvening { get; set; }    
    }
}

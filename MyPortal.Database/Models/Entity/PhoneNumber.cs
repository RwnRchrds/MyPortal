using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("PhoneNumbers")]
    public class PhoneNumber : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid TypeId { get; set; }
        
        [Column(Order = 2)]
        public Guid? PersonId { get; set; }

        [Column(Order = 3)] 
        public Guid? AgencyId { get; set; }

        [Column(Order = 4)]
        [Phone]
        [StringLength(128)]
        public string Number { get; set; }
        
        [Column(Order = 5)] 
        public bool Main { get; set; }

        public virtual PhoneNumberType Type { get; set; }
        public virtual Person Person { get; set; }
    }
}
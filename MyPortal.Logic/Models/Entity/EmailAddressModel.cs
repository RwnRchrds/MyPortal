using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class EmailAddressModel : BaseModel
    {
        public Guid TypeId { get; set; }
        
        public Guid? PersonId { get; set; }
        
        public Guid? AgencyId { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(128)]
        public string Address { get; set; }
        
        public bool Main { get; set; }
        
        public string Notes { get; set; }

        public virtual PersonModel Person { get; set; }
        public virtual EmailAddressTypeModel Type { get; set; }
    }
}
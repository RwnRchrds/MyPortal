using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.BaseTypes
{
    public abstract class LookupItem : Entity, ILookupItem
    {
        public LookupItem()
        {
            Active = true;
        }
        
        [Required]
        [Column(Order = 1)]
        [StringLength(256)]
        public string Description { get; set; }

        [Column(Order = 2)]
        public bool Active { get; set; }
    }
}

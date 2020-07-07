using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class RoomModel
    {
        public Guid Id { get; set; }
        
        public Guid? LocationId { get; set; }
        
        [StringLength(10)]
        public string Code { get; set; }
        
        [StringLength(256)]
        public string Name { get; set; }
        
        public int MaxGroupSize { get; set; }
        
        public string TelephoneNo { get; set; }
        
        public bool ExcludeFromCover { get; set; }

        public virtual LocationModel Location { get; set; }
    }
}
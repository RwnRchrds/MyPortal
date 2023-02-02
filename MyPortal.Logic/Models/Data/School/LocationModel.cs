using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.School
{
    public class LocationModel : BaseModel
    {
        public LocationModel(Location model) : base(model)
        {
            Description = model.Description;
            System = model.System;
        }
        
        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public bool System { get; set; }
    }
}

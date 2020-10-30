using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Data
{
    public class LookupItemModel : BaseModel
    {
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(256)]
        public string Description { get; set; }
        
        public bool Active { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;

namespace MyPortal.Logic.Models.Data
{
    public class LookupItemModel : BaseModel, ILookupItem
    {
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(256)]
        public string Description { get; set; }
        
        public bool Active { get; set; }
    }
}

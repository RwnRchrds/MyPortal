using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class SenStatusModel : LookupItemModel
    {
        [Required]
        [StringLength(1)]
        public string Code { get; set; }

        public bool Active { get; set; }
    }
}
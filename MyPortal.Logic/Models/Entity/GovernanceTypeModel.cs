using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class GovernanceTypeModel : LookupItemModel
    {
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class IntakeTypeModel : LookupItemModel
    {
        public IntakeTypeModel(IntakeType model) : base(model)
        {
            Code = model.Code;
        }
        
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
    }
}

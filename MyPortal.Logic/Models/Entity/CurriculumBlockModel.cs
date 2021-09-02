using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class CurriculumBlockModel : BaseModel
    {
        public CurriculumBlockModel(CurriculumBlock model) : base(model)
        {
            Code = model.Code;
            Description = model.Description;
        }
        
        [StringLength(10)]
        public string Code { get; set; }
        
        [StringLength(256)]
        public string Description { get; set; }
    }
}
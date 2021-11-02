using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class SchoolTypeModel : LookupItemModel
    {
        public SchoolTypeModel(SchoolType model) : base(model)
        {
            Code = model.Code;
        }
        
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
    }
}
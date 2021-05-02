using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class StudentGroupTypeModel : LookupItemModel
    {
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
        
        public bool AllowSimultaneous { get; set; }
        
        public bool AllowPromotion { get; set; }
    }
}
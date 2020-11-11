using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class EthnicityModel : LookupItemModel
    {
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
    }
}
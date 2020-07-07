using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class TaskTypeModel : LookupItemModel
    {
        public bool Personal { get; set; }

        public bool System { get; set; }

        public bool Reserved { get; set; }

        [Required]
        public string ColourCode { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class LogNoteTypeModel : LookupItemModel

    {
        [Required]
        [StringLength(128)] 
        public string ColourCode { get; set; }

        [Required]
        public string IconClass { get; set; }
    }
}
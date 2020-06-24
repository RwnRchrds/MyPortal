using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Constants;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Business
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
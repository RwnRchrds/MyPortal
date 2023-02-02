using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.StaffMembers
{
    public class ObservationOutcomeModel : LookupItemModel
    {
        public ObservationOutcomeModel(ObservationOutcome model) : base(model)
        {
            ColourCode = model.ColourCode;
        }
        
        [StringLength(128)]
        public string ColourCode { get; set; }
    }
}

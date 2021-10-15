using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ObservationOutcomeModel : LookupItemModel
    {
        [StringLength(128)]
        public string ColourCode { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Business
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

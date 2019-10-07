using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Models.Database
{
    [Table("School_IntakeTypes")]
    public class SchoolIntakeType
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
    }
}
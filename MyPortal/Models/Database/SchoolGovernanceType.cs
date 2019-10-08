using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Models.Database
{
    [Table("School_GovernanceTypes")]
    public class SchoolGovernanceType
    {
        public int Id { get; set; }

        public string Description { get; set; }
    }
}
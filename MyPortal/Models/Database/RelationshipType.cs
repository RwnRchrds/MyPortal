using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Models.Database
{
    [Table("People_RelationshipTypes")]
    public class RelationshipType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }
    }
}
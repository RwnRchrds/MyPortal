using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Models.Database
{
    /// <summary>
    /// A medical condition a person may have.
    /// </summary>
    [Table("Medical_Conditions")]
    public class MedicalCondition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MedicalCondition()
        {
            
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public bool System { get; set; }
    }
}
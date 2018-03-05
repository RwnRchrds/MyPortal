using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    public class StudentDto
    {
        //ID Provided to Student by SIMS .net (MIS ID) --> Student *MUST* Exist in MIS before adding to MyPortal
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        //ID of Student in the 4Matrix Database --> Used for links between MyPortal and 4Matrix
        [Display(Name = "4Matrix ID")]
        public int? FourMId { get; set; }

        [Required]
        [StringLength(3)]
        public int RegGroup { get; set; }

        [Required]
        [StringLength(50)]
        public int YearGroup { get; set; }

        [Display(Name = "Account Balance")]
        public decimal AccountBalance { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    [Table("Sen_ReviewTypes")]
    public class SenReviewType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }
    }
}
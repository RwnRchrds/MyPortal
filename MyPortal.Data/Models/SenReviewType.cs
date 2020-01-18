﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    [Table("SenReviewType")]
    public class SenReviewType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }
    }
}
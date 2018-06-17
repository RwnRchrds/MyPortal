using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPortal.Models
{
    public class BalanceAdjustment
    {        
        [Required]
        public int Student { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
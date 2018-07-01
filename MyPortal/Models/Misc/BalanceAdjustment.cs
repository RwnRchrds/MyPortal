using System.ComponentModel.DataAnnotations;

namespace MyPortal.Models.Misc
{
    public class BalanceAdjustment
    {        
        [Required]
        public int Student { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
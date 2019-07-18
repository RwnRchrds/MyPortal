using System.ComponentModel.DataAnnotations;

namespace MyPortal.Models.Misc
{
    public class FinanceTransaction
    {
        [Required] public int StudentId { get; set; }

        [Required] public decimal Amount { get; set; }
    }
}
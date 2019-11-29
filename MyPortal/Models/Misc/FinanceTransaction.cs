using System.ComponentModel.DataAnnotations;

namespace MyPortal.Models.Misc
{
    public class FinanceTransaction
    {
        public int StudentId { get; set; }

        public decimal Amount { get; set; }
    }
}
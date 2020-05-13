using System;

namespace MyPortal.Logic.Models.Summary
{
    public class SaleListModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string StudentName { get; set; }
        public string ProductDescription { get; set; }
        public decimal AmountPaid { get; set; }
        public bool Refunded { get; set; }
        public bool Processed { get; set; }
    }
}
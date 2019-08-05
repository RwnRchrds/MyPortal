namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Represents a product in a student's basket.
    /// </summary>
    [Table("Finance_BasketItems")]
    public partial class FinanceBasketItem
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProductId { get; set; }

        public virtual Student Student { get; set; }

        public virtual FinanceProduct FinanceProduct { get; set; }
    }
}

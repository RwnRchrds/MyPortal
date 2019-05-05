namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Finance_BasketItems")]
    public partial class FinanceBasketItem
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProductId { get; set; }

        public virtual PeopleStudent CoreStudent { get; set; }

        public virtual FinanceProduct FinanceProduct { get; set; }
    }
}

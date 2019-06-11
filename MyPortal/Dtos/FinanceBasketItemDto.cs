namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Represents a product in a student's basket.
    /// </summary>
    public partial class FinanceBasketItemDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProductId { get; set; }

        public virtual StudentDto CoreStudent { get; set; }

        public virtual FinanceProductDto FinanceProduct { get; set; }
    }
}

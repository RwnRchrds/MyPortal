namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A product available for purchase on the system.
    /// </summary>
    
    public partial class FinanceProductDto
    {
        public int Id { get; set; }

        public int ProductTypeId { get; set; }

        
        public string Description { get; set; }

        
        public decimal Price { get; set; }

        public bool Visible { get; set; }

        public bool OnceOnly { get; set; }

        public bool Deleted { get; set; }

        public virtual FinanceProductTypeDto FinanceProductType { get; set; }

        
        

        
        
    }
}

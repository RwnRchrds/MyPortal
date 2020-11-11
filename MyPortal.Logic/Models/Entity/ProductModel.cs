using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ProductModel : BaseModel
    {
        public Guid ProductTypeId { get; set; }

        public Guid VatRateId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool ShowOnStore { get; set; }

        [Range(0, Int32.MaxValue)]
        public int OrderLimit { get; set; }

        public bool Deleted { get; set; }

        public virtual ProductTypeModel Type { get; set; }

        public virtual VatRateModel VatRate { get; set; }
    }
}

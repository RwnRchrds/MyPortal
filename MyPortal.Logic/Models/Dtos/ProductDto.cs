using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public int ProductTypeId { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool Visible { get; set; }

        public bool OnceOnly { get; set; }

        public bool Deleted { get; set; }

        public virtual ProductTypeDto Type { get; set; }
    }
}

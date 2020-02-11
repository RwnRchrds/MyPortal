using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class BasketItemDto
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid ProductId { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual ProductDto Product { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class BasketItemDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProductId { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual ProductDto Product { get; set; }
    }
}

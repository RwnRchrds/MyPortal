using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    public class SaleDto
    {
        public int Id { get; set; }
        public int Student { get; set; }
        public int Product { get; set; }
        public DateTime Date { get; set; }

        public StudentDto Student1 { get; set; }
        public ProductDto Product1 { get; set; }
    }
}
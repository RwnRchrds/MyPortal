using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos.GridDtos
{
    public class GridFinanceProductDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string TypeDescription { get; set; }
        public decimal Price { get; set; }
        public bool Visible { get; set; }
        public bool OnceOnly { get; set; }
    }
}
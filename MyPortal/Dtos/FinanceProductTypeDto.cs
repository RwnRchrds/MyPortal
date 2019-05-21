using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    public class FinanceProductTypeDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsMeal { get; set; }
        public bool System { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    /// <summary>
    /// Type of product in the system.
    /// </summary>
    
    public partial class FinanceProductTypeDto
    {
        public int Id { get; set; }

        
        public string Description { get; set; }

        public bool IsMeal { get; set; }

        public bool System { get; set; }

        
        
    }
}
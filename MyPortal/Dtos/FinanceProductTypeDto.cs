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
        /// <summary>
        /// Indicates whether the product is a meal. Students entitled to FSM will not be charged for meals.
        /// </summary>
        public bool IsMeal { get; set; }
        /// <summary>
        /// Indicates whether the product is a system product. System products cannot be modified or deleted.
        /// </summary>
        public bool System { get; set; }
    }
}
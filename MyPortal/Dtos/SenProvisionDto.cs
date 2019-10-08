using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    
    public class SenProvisionDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProvisionTypeId { get; set; }

        
        public DateTime StartDate { get; set; }

        
        public DateTime EndDate { get; set; }

        
        public string Note { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual SenProvisionTypeDto SenProvisionType { get; set; }
    }
}
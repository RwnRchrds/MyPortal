using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    /// <summary>
    ///  A status of special educational needs a student may have.
    /// </summary>
    
    public partial class SenStatusDto
    {
        public int Id { get; set; }

        
        
        public string Code { get; set; }

        
        public string Description { get; set; }

        
        
    }
}
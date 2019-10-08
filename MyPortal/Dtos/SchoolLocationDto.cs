using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    /// <summary>
    /// Location where an achievement/behaviour incident took place.
    /// </summary>
    
    public class SchoolLocationDto
    {
        public int Id { get; set; }

        
        public string Description { get; set; }

        
        

        
        
    }
}
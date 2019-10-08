using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    /// <summary>
    /// Type of communication.
    /// </summary>
    
    public class CommunicationTypeDto
    {

        public int Id { get; set; }

        
        public string Description { get; set; }

        
        
    }
}
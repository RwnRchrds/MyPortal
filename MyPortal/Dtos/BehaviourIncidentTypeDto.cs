using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    /// <summary>
    /// Category of behaviour incident.
    /// </summary>
    
    public class BehaviourIncidentTypeDto
    {
        public int Id { get; set; }

        
        public string Description { get; set; }

        public bool System { get; set; }

        public int DefaultPoints { get; set; }

        
        
    }
}
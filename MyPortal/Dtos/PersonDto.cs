using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    /// <summary>
    /// Represents an individual person in the system.
    /// </summary>
    
    public class PersonDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string NhsNumber { get; set; }
        
        public string LastName { get; set; }

        public int? PhotoId { get; set; }

        public string Gender { get; set; }

        public DateTime? Dob { get; set; }

        public DateTime? Deceased { get; set; }

        public string UserId { get; set; }

        public bool Deleted { get; set; }

        
        

        
        

        
        

        
        
    }
}
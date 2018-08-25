using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyPortal.Models;

namespace MyPortal.Dtos
{
    public class StaffObservationDto
    {
        public int Id { get; set; }

        [Required]    
        public DateTime Date { get; set; }
        
        [Required]
        public string Observee { get; set; }
        
        [Required]
        public string Observer { get; set; }
        
        [Required]
        public string Outcome { get; set; }

        
        public StaffDto Staff { get; set; }
        
        public StaffDto Staff1 { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    /// <summary>
    /// A record of a medical event/emergency.
    /// </summary>
    
    public class MedicalEventDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int RecordedById { get; set; }

        
        public DateTime Date { get; set; }

        
        public string Note { get; set; }

        public virtual StaffMemberDto RecordedBy { get; set; }

        public virtual StudentDto Student { get; set; }
    }
}
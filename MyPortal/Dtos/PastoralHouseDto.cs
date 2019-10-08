using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    /// <summary>
    /// A school house.
    /// </summary>
    
    public class PastoralHouseDto
    {
        public int Id { get; set; }

        
        public string Name { get; set; }

        public int HeadId { get; set; }

        public virtual StaffMemberDto HeadOfHouse { get; set; }

        
        
    }
}
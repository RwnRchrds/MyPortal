using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Dtos
{
    /// <summary>
    /// A medical condition a person may have.
    /// </summary>
    
    public class MedicalConditionDto
    {
        
        public MedicalConditionDto()
        {
            
        }

        public int Id { get; set; }

        
        public string Description { get; set; }

        public bool System { get; set; }
    }
}
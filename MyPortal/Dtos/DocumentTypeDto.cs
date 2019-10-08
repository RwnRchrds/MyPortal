using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    /// <summary>
    /// Type of document
    /// </summary>
    
    public class DocumentTypeDto
    {
        public int Id { get; set; }

        
        public string Description { get; set; }

        
        
    }
}
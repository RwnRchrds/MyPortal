using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class DocumentTypeDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public bool Person { get; set; }
        public bool General { get; set; }
        public bool Sen { get; set; }
    }
}

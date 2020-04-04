using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Business
{
    public class DocumentTypeModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public bool Staff { get; set; }
        public bool Student { get; set; }
        public bool Contact { get; set; }
        public bool General { get; set; }
        public bool Sen { get; set; }

        public bool Active { get; set; }
    }
}

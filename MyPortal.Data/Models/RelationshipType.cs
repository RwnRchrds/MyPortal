﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    [Table("RelationshipType", Schema = "person")]
    public class RelationshipType
    {
        public RelationshipType()
        {
            StudentContacts = new HashSet<StudentContact>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public ICollection<StudentContact> StudentContacts { get; set; }
    }
}
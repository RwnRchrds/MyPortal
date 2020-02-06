﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("RelationshipType")]
    public class RelationshipType
    {
        public RelationshipType()
        {
            StudentContacts = new HashSet<StudentContact>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public ICollection<StudentContact> StudentContacts { get; set; }
    }
}
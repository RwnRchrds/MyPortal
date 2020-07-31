﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ContactRelationshipTypes")]
    public class ContactRelationshipType : LookupItem
    {
        public ContactRelationshipType()
        {
            StudentContacts = new HashSet<StudentContactRelationship>();
        }

        public ICollection<StudentContactRelationship> StudentContacts { get; set; }
    }
}
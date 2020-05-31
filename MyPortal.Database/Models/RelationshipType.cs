using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("RelationshipType")]
    public class RelationshipType : LookupItem
    {
        public RelationshipType()
        {
            StudentContacts = new HashSet<StudentContact>();
        }

        public ICollection<StudentContact> StudentContacts { get; set; }
    }
}
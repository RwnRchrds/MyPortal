﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Contacts")]
    public class Contact : BaseTypes.Entity
    {
        public Contact()
        {
            LinkedStudents = new HashSet<StudentContactRelationship>();
        }

        [Column(Order = 2)] public Guid PersonId { get; set; }

        [Column(Order = 3)] public bool ParentalBallot { get; set; }

        [Column(Order = 4)]
        [StringLength(256)]
        public string PlaceOfWork { get; set; }

        [Column(Order = 5)]
        [StringLength(256)]
        public string JobTitle { get; set; }

        [Column(Order = 6)]
        [StringLength(128)]
        public string NiNumber { get; set; }

        public virtual Person Person { get; set; }


        public virtual ICollection<StudentContactRelationship> LinkedStudents { get; set; }
    }
}
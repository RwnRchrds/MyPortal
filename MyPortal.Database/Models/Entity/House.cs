using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Houses")]
    public class House : BaseTypes.Entity, IStudentGroupEntity
    {
        [Column(Order = 1)] 
        public Guid StudentGroupId { get; set; }

        [Column(Order = 2)]
        [StringLength(10)]
        public string ColourCode { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }
    }
}
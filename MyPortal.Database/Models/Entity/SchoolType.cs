﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("SchoolTypes")]
    public class SchoolType : LookupItem, ICensusEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchoolType()
        {
            Schools = new HashSet<School>();
        }

        [Column(Order = 4)]
        [Required]
        [StringLength(10)]
        public string Code { get; set; }


        public virtual ICollection<School> Schools { get; set; }
    }
}
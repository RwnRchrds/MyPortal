using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace MyPortal.Database.BaseTypes
{
    public abstract class LookupItem
    {
        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [DataMember]
        [StringLength(256)]
        public string Description { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}

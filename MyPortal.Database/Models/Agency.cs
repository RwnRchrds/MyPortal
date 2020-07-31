using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Agencies")]
    public class Agency : Entity, ISoftDeleteEntity
    {
        public Agency()
        {
            Agents = new HashSet<Agent>();
        }

        [Column(Order = 1)]
        public Guid TypeId { get; set; }

        [Column(Order = 2)]
        public Guid? AddressId { get; set; }

        [Column(Order = 3)]
        public Guid DirectoryId { get; set; }

        [Column(Order = 4)]
        [StringLength(256)]
        public string Name { get; set; }

        [Column(Order = 5)]
        [Url]
        [StringLength(100)]
        public string Website { get; set; }

        [Column(Order = 6)]
        public bool Deleted { get; set; }

        public virtual AgencyType AgencyType { get; set; }
        public virtual Address Address { get; set; }
        public virtual Directory Directory { get; set; }
        public virtual ICollection<Agent> Agents { get; set; }
    }
}

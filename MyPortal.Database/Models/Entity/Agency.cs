﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Agencies")]
    public class Agency : BaseTypes.Entity, IDirectoryEntity, ISoftDeleteEntity
    {
        public Agency()
        {
            Agents = new HashSet<Agent>();
        }

        [Column(Order = 2)] public Guid TypeId { get; set; }

        [Column(Order = 3)] public Guid DirectoryId { get; set; }

        [Column(Order = 4)]
        [StringLength(256)]
        public string Name { get; set; }

        [Column(Order = 5)]
        [Url]
        [StringLength(100)]
        public string Website { get; set; }

        [Column(Order = 6)] public bool Deleted { get; set; }

        public virtual AgencyType AgencyType { get; set; }
        public virtual Directory Directory { get; set; }
        public virtual ICollection<Agent> Agents { get; set; }
        public virtual ICollection<School> Schools { get; set; }
        public virtual ICollection<AddressAgency> AddressAgencies { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual ICollection<EmailAddress> EmailAddresses { get; set; }
    }
}
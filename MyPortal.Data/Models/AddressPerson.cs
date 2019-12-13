﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    [Table("AddressPerson", Schema = "communication")]
    public class AddressPerson
    {
        public int Id { get; set; }

        public int AddressId { get; set; }

        public int PersonId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Person Person { get; set; }
    }
}
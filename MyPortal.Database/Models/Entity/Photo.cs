using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Photos")]
    public class Photo : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public byte[] Data { get; set; }

        [Column(Order = 2)]
        public DateTime PhotoDate { get; set; }

        [Column(Order = 3)]
        public string MimeType { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}

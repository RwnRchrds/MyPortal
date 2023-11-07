using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Photos")]
    public class Photo : BaseTypes.Entity
    {
        [Column(Order = 2)] public byte[] Data { get; set; }

        [Column(Order = 3, TypeName = "date")] public DateTime PhotoDate { get; set; }

        [Column(Order = 4)] public string MimeType { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
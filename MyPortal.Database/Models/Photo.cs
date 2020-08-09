using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("Photos")]
    public class Photo : Entity
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

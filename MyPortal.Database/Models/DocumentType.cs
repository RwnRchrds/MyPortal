using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("DocumentTypes")]
    public class DocumentType : LookupItem
    {
        public DocumentType()
        {
            Documents = new HashSet<Document>();
        }

        [Column(Order = 3)]
        public bool Staff { get; set; }

        [Column(Order = 4)]
        public bool Student { get; set; }

        [Column(Order = 5)]
        public bool Contact { get; set; }

        [Column(Order = 6)]
        public bool General { get; set; }

        [Column(Order = 7)]
        public bool Sen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Documents { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("DocumentType")]
    public class DocumentType : LookupItem
    {
        public DocumentType()
        {
            Documents = new HashSet<Document>();
        }

        public bool Staff { get; set; }
        public bool Student { get; set; }
        public bool Contact { get; set; }
        public bool General { get; set; }
        public bool Sen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Documents { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
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

        [DataMember]
        public bool Staff { get; set; }

        [DataMember]
        public bool Student { get; set; }

        [DataMember]
        public bool Contact { get; set; }

        [DataMember]
        public bool General { get; set; }

        [DataMember]
        public bool Sen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Documents { get; set; }
    }
}
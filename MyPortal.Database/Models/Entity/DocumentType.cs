using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("DocumentTypes")]
    public class DocumentType : LookupItem, ISystemEntity
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
        
        [Column(Order = 8)] 
        public bool System { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Documents { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Rooms")]
    public class Room : Entity
    {
        public Room()
        {
            Sessions = new HashSet<Session>();
            CoverArrangements = new HashSet<CoverArrangement>();
            Closures = new HashSet<RoomClosure>();
        }

        [Column(Order = 1)]
        public Guid? LocationId { get; set; }

        [Column(Order = 2)]
        [StringLength(10)]
        public string Code { get; set; }

        [Column(Order = 3)]
        [StringLength(256)]
        public string Name { get; set; }

        [Column(Order = 4)]
        public int MaxGroupSize { get; set; }

        [Column(Order = 5)]
        public string TelephoneNo { get; set; }

        [Column(Order = 6)]
        public bool ExcludeFromCover { get; set; }

        public virtual Location Location { get; set; }

        public virtual ExamRoom ExamRoom { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Session> Sessions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoverArrangement> CoverArrangements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiaryEvent> DiaryEvents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomClosure> Closures { get; set; }
    }
}

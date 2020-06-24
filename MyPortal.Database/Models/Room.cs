using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("Room")]
    public class Room
    {
        public Room()
        {
            Sessions = new HashSet<Session>();
            CoverArrangements = new HashSet<Cover>();
            Closures = new HashSet<RoomClosure>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid Id { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Session> Sessions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cover> CoverArrangements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiaryEvent> DiaryEvents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomClosure> Closures { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Rooms")]
    public class Room : BaseTypes.Entity
    {
        public Room()
        {
            Sessions = new HashSet<Session>();
            CoverArrangements = new HashSet<CoverArrangement>();
            Closures = new HashSet<RoomClosure>();
        }

        [Column(Order = 2)]
        public Guid? BuildingFloorId { get; set; }

        [Column(Order = 3)]
        [StringLength(10)]
        public string Code { get; set; }

        [Column(Order = 4)]
        [StringLength(256)]
        public string Name { get; set; }

        [Column(Order = 5)]
        public int MaxGroupSize { get; set; }

        [Column(Order = 6)]
        public string TelephoneNo { get; set; }

        [Column(Order = 7)]
        public bool ExcludeFromCover { get; set; }

        public virtual BuildingFloor BuildingFloor { get; set; }

        public virtual ICollection<ExamRoom> ExamRooms { get; set; }

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

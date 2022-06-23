﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Bulletins")]
    public class Bulletin : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid DirectoryId { get; set; }

        [Column(Order = 2)]
        public Guid CreatedById { get; set; }

        [Column(Order = 3)]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 4)]
        public DateTime? ExpireDate { get; set; }

        [Column(Order = 5)]
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Column(Order = 6)]
        [Required]
        public string Detail { get; set; }
            
        [Column(Order = 7)]
        public bool Private { get; set; }

        [Column(Order = 8)]
        public bool Approved { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual Directory Directory { get; set; }
    }
}
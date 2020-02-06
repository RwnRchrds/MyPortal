﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("MedicalEvent")]
    public class MedicalEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid RecordedById { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        public string Note { get; set; }

        public virtual StaffMember RecordedBy { get; set; }

        public virtual Student Student { get; set; }
    }
}
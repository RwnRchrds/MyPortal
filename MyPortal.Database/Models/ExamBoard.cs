using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("ExamBoards")]
    public class ExamBoard : Entity
    {
        // TODO: Populate Data

        [Column(Order = 2)]
        [StringLength(20)]
        public string Abbreviation { get; set; }

        [Column(Order = 3)]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 4)]
        [StringLength(5)]
        public string Code { get; set; }

        [Column(Order = 5)]
        public bool Domestic { get; set; }

        [Column(Order = 6)]
        public bool Active { get; set; }

        public virtual ICollection<ExamSeries> ExamSeries { get; set; }
    }
}

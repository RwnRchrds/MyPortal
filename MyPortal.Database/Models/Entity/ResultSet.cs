using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("ResultSets")]
    public class ResultSet : LookupItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ResultSet()
        {
            Results = new HashSet<Result>();
        }

        [Column(Order = 4)]
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Column(Order = 5, TypeName = "date")] public DateTime? PublishDate { get; set; }

        [Column(Order = 6)] public bool Locked { get; set; }


        public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<ExamSeason> ExamSeasons { get; set; }
        public virtual ICollection<ExamResultEmbargo> ExamResultEmbargoes { get; set; }
    }
}
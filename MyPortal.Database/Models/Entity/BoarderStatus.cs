using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("BoarderStatus")]
    public class BoarderStatus : LookupItem, ICensusEntity
    {
        public BoarderStatus()
        {
            Students = new HashSet<Student>();
        }

        [Column(Order = 4)] public string Code { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
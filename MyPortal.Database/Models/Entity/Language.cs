using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Languages")]
    public class Language : LookupItem, ICensusEntity
    {
        [Column(Order = 4)] public string Code { get; set; }
    }
}
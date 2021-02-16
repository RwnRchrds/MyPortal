using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Languages")]
    public class Language : LookupItem, ICensusEntity
    {
        [Column(Order = 3)]
        public string Code { get; set; }
    }
}

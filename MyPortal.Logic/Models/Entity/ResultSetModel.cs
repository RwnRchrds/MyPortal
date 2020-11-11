using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ResultSetModel : LookupItemModel
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
    }
}

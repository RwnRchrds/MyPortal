using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ResultSetModel : LookupItemModel
    {
        public ResultSetModel(ResultSet model) : base(model)
        {
            Name = model.Name;
        }
        
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
    }
}

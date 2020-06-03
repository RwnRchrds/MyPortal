using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("SystemSetting")]
    public class SystemSetting
    {
        [Key]
        public string Name { get; set; }
        public string Type { get; set; }
        public string Setting { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("SystemSetting")]
    public class SystemSetting
    {
        [DataMember]
        [Key]
        public string Name { get; set; }
        
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Setting { get; set; }
    }
}

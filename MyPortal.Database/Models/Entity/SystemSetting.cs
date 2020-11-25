using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("SystemSettings")]
    public class SystemSetting
    {
        [Column(Order = 0)]
        [Key]
        public string Name { get; set; }
        
        [Column(Order = 1)]
        public string Type { get; set; }

        [Column(Order = 2)]
        public string Setting { get; set; }
    }
}

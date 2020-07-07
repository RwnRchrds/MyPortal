using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class BulletinModel
    {
        public Guid Id { get; set; }
        
        public Guid DirectoryId { get; set; }
        
        public Guid AuthorId { get; set; }
        
        public DateTime CreateDate { get; set; }
        
        public DateTime? ExpireDate { get; set; }
        
        [Required]
        [StringLength(128)]
        public string Title { get; set; }
        
        [Required]
        public string Detail { get; set; }
        
        public bool StaffOnly { get; set; }
        
        public bool Approved { get; set; }

        public virtual UserModel Author { get; set; }
        public virtual DirectoryModel Directory { get; set; }
    }
}

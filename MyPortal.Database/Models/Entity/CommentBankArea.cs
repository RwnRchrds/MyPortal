using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity;

[Table("CommentBankAreas")]
public class CommentBankArea : BaseTypes.Entity
{ 
    [Column(Order = 1)]
    public Guid CommentBankId { get; set; }
    
    [Column(Order = 2)]
    public Guid CourseId { get; set; }
    
    [Required]
    [StringLength(256)]
    [Column(Order = 3)]
    public string Name { get; set; }

    public virtual CommentBank CommentBank { get; set; }
    public virtual Course Course { get; set; }
    public virtual ICollection<CommentBankSection> Sections { get; set; }
}
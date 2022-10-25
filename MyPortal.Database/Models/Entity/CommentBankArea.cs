using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Database.Models.Entity;

public class CommentBankArea : BaseTypes.Entity
{ 
    public Guid CommentBankId { get; set; }
    public Guid CourseId { get; set; }
    
    [Required]
    [StringLength(256)]
    public string Name { get; set; }

    public virtual CommentBank CommentBank { get; set; }
    public virtual Course Course { get; set; }
    public virtual ICollection<CommentBankSection> Sections { get; set; }
}
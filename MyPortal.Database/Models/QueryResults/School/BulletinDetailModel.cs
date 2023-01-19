using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Database.Models.QueryResults.School;

public class BulletinDetailModel
{
    public Guid Id { get; set; }
    
    public Guid DirectoryId { get; set; }
    
    public Guid CreatedById { get; set; }

    public string CreatedByName { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime? ExpireDate { get; set; }
    
    public string Title { get; set; }
    
    public string Detail { get; set; }
    
    public bool Private { get; set; }
    
    public bool Approved { get; set; }
}
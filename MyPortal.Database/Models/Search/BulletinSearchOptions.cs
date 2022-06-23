using System;

namespace MyPortal.Database.Models.Search;

public class BulletinSearchOptions
{
    public string SearchText { get; set; }
    public bool IncludeStaffOnly { get; set; }
    public bool IncludeExpired { get; set; }
    public bool IncludeUnapproved { get; set; }
    public Guid? IncludeCreatedBy { get; set; }
}
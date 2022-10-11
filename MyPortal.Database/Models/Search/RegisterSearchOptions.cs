using System;

namespace MyPortal.Database.Models.Search;

public class RegisterSearchOptions
{
    public Guid? TeacherId { get; set; }
    public Guid? PeriodId { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}
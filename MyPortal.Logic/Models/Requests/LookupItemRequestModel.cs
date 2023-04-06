using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests;

public class LookupItemRequestModel
{
    [StringLength(256)]
    public string Description { get; set; }
    public bool Active { get; set; }
}
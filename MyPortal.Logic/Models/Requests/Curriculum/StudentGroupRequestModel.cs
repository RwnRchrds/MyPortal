using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests.Curriculum;

public class StudentGroupRequestModel
{
    public string Code { get; set; }
    
    public Guid? PromoteToGroupId { get; set; }
    
    public int? MaxMembers { get; set; }
    
    [StringLength(256)]
    public string Notes { get; set; }
    
    public bool System { get; set; }
}
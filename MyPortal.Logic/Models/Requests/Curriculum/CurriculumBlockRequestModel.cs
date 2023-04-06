using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests.Curriculum;

public class CurriculumBlockRequestModel
{
    [StringLength(10)]
    public string Code { get; set; }
    
    [StringLength(256)]
    public string Description { get; set; }
}
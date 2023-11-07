using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.Assessment;

public class AspectRequestModel
{
    [NotDefault] public Guid TypeId { get; set; }

    [RequiredIfGradeAspect] public Guid? GradeSetId { get; set; }

    [RequiredIfMarkAspect] public decimal? MinMark { get; set; }

    [RequiredIfMarkAspect] public decimal? MaxMark { get; set; }

    [Required] [StringLength(128)] public string Name { get; set; }

    [Required] [StringLength(50)] public string ColumnHeading { get; set; }

    public bool Private { get; set; }
}
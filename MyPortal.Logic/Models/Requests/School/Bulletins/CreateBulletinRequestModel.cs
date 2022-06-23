﻿using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.School.Bulletins;

public class CreateBulletinRequestModel
{
    [Required]
    [StringLength(50)]
    public string Title { get; set; }
    
    [Required]
    public string Detail { get; set; }
    
    public bool Private { get; set; }
    
    public DateTime? ExpireDate { get; set; }
    
    [NotEmpty]
    public Guid CreatedById { get; set; }
}
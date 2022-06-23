using System;

namespace MyPortal.Logic.Models.Requests.School.Bulletins;

public class ApproveBulletinRequestModel
{
    public Guid BulletinId { get; set; }
    public bool Approved { get; set; }
}
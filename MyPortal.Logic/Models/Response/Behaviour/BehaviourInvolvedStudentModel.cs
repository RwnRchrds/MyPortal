using System;

namespace MyPortal.Logic.Models.Response.Behaviour;

public class BehaviourInvolvedStudentModel
{
    public Guid StudentId { get; set; }
    public Guid RoleTypeId { get; set; }
    public string StudentName { get; set; }
    public string RoleTypeName { get; set; }
    public int Points { get; set; }
}
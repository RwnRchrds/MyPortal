using System;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Requests.Curriculum;

namespace MyPortal.Logic.Helpers;

public static class StudentGroupHelper
{
    public static StudentGroup CreateStudentGroupFromModel(StudentGroupRequestModel model)
    {
        return new StudentGroup
        {
            Id = Guid.NewGuid(),
            Active = model.Active,
            Description = model.Description,
            Code = model.Code,
            PromoteToGroupId = model.PromoteToGroupId,
            MainSupervisorId = model.MainSupervisorId,
            MaxMembers = model.MaxMembers,
            Notes = model.Notes,
            System = model.System
        };
    }

    public static void UpdateStudentGroupFromModel(StudentGroup studentGroup, StudentGroupRequestModel model)
    {
        studentGroup.Active = model.Active;
        studentGroup.Description = model.Description;
        studentGroup.Code = model.Code;
        studentGroup.PromoteToGroupId = model.PromoteToGroupId;
        studentGroup.MainSupervisorId = model.MainSupervisorId;
        studentGroup.MaxMembers = model.MaxMembers;
        studentGroup.Notes = model.Notes;
        studentGroup.System = model.System;
    }
}
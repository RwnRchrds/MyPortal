using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.StaffMembers;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Curriculum;

public class StudentGroupSupervisorModel : BaseModel
{
    public StudentGroupSupervisorModel(StudentGroupSupervisor model) : base(model)
    {
        LoadFromModel(model);
    }

    private void LoadFromModel(StudentGroupSupervisor model)
    {
        StudentGroupId = model.StudentGroupId;
        SupervisorId = model.SupervisorId;
        Title = model.Title;

        if (model.StudentGroup != null)
        {
            StudentGroup = new StudentGroupModel(model.StudentGroup);
        }

        if (model.Supervisor != null)
        {
            Supervisor = new StaffMemberModel(model.Supervisor);
        }
    }

    public async Task Load(IUnitOfWork unitOfWork)
    {
        if (Id.HasValue)
        {
            var groupSupervisor = await unitOfWork.StudentGroupSupervisors.GetById(Id.Value);

            if (groupSupervisor != null)
            {
                LoadFromModel(groupSupervisor);
            }
        }
    }
    
    public Guid StudentGroupId { get; set; }
    public Guid SupervisorId { get; set; }
    public string Title { get; set; }

    public StudentGroupModel StudentGroup { get; set; }
    public StaffMemberModel Supervisor { get; set; }
}
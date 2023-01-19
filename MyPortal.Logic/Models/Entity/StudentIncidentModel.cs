using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Summary;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity;

public class StudentIncidentModel : BaseModelWithLoad
{
    public StudentIncidentModel(StudentIncident model) : base(model)
    {
        LoadFromModel(model);
    }

    private void LoadFromModel(StudentIncident model)
    {
        StudentId = model.StudentId;
        IncidentId = model.IncidentId;
        RoleTypeId = model.RoleTypeId;
        OutcomeId = model.OutcomeId;
        StatusId = model.StatusId;
        Points = model.Points;

        if (model.Student != null)
        {
            Student = new StudentModel(model.Student);
        }

        if (model.Incident != null)
        {
            Incident = new IncidentModel(model.Incident);
        }

        if (model.RoleType != null)
        {
            RoleType = new BehaviourRoleTypeModel(model.RoleType);
        }

        if (model.Outcome != null)
        {
            Outcome = new BehaviourOutcomeModel(model.Outcome);
        }

        if (model.Status != null)
        {
            Status = new BehaviourStatusModel(model.Status);
        }
    }

    public Guid StudentId { get; set; }

    public Guid IncidentId { get; set; }

    public Guid RoleTypeId { get; set; }

    public Guid? OutcomeId { get; set; }

    public Guid StatusId { get; set; }

    public int Points { get; set; }
    
    public StudentModel Student { get; set; }
    public IncidentModel Incident { get; set; }
    public BehaviourRoleTypeModel RoleType { get; set; }
    public BehaviourOutcomeModel Outcome { get; set; }
    public BehaviourStatusModel Status { get; set; }
    
    public BehaviourInvolvedStudentSummaryModel[] InvolvedStudents { get; set; }

    protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
    {
        if (Id.HasValue)
        {
            var model = await unitOfWork.StudentIncidents.GetById(Id.Value);
            
            LoadFromModel(model);
        }
    }
    
    internal async Task<StudentIncidentSummaryModel> ToListModel(IUnitOfWork unitOfWork)
    {
        return await StudentIncidentSummaryModel.GetSummary(unitOfWork, this);
    }
}
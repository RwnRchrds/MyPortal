using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Attendance;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Curriculum;

public class SessionPeriodModel : BaseModelWithLoad
{
    public SessionPeriodModel(SessionPeriod model) : base(model)
    {
    }

    private void LoadFromModel(SessionPeriod model)
    {
        SessionId = model.SessionId;
        PeriodId = model.PeriodId;

        if (model.Session != null)
        {
            Session = new SessionModel(model.Session);
        }

        if (model.Period != null)
        {
            Period = new AttendancePeriodModel(model.Period);
        }
    }

    public Guid SessionId { get; set; }
    public Guid PeriodId { get; set; }

    public virtual SessionModel Session { get; set; }
    public virtual AttendancePeriodModel Period { get; set; }


    protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
    {
        if (Id.HasValue)
        {
            var model = await unitOfWork.SessionPeriods.GetById(Id.Value);

            LoadFromModel(model);
        }
    }
}
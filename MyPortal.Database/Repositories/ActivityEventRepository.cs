using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class ActivityEventRepository : BaseReadWriteRepository<ActivityEvent>, IActivityEventRepository
    {
        public ActivityEventRepository(ApplicationDbContext context) : base(context, "AE")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Activity), "A");
            query.SelectAllColumns(typeof(DiaryEvent), "E");
            query.SelectAllColumns(typeof(DiaryEventType), "ET");
            query.SelectAllColumns(typeof(Room), "R");
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin(EntityHelper.GetTableName(typeof(Activity), "A"), "A.Id", "AE.ActivityId");
            query.LeftJoin(EntityHelper.GetTableName(typeof(DiaryEvent), "E"), "E.Id", "AE.EventId");
        }

        public IEnumerable<ActivityEvent> GetByStudent(Guid studentId, DateTime dateFrom, DateTime dateTo)
        {
            throw new NotImplementedException();
        }
    }
}

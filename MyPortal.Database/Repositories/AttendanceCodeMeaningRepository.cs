using System.Data.Common;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class AttendanceCodeMeaningRepository : BaseReadRepository<AttendanceCodeMeaning>, IAttendanceCodeMeaningRepository
    {
        public AttendanceCodeMeaningRepository(DbTransaction transaction) : base(transaction, "ACM")
        {
        }
    }
}
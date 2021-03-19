using System.Data.Common;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class AcademicTermRepository : BaseReadWriteRepository<AcademicTerm>, IAcademicTermRepository
    {
        public AcademicTermRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "AT")
        {
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AcademicYear), "AY");
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("AcademicYears AS AY", "AY.Id", "AT.AcademicYearId");
        }
    }
}

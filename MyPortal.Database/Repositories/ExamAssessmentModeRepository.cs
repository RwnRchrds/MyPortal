using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class ExamAssessmentModeRepository : BaseReadRepository<ExamAssessmentMode>, IExamAssessmentModeRepository
    {
        public ExamAssessmentModeRepository(DbTransaction transaction, string tblAlias = null) : base(transaction, tblAlias)
        {
        }
    }
}
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class LessonPlanTemplateRepository : BaseReadWriteRepository<LessonPlanTemplate>, ILessonPlanTemplateRepository
    {
        public LessonPlanTemplateRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        }

        protected override async Task<IEnumerable<LessonPlanTemplate>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<LessonPlanTemplate>(sql, param);
        }

        public async Task Update(LessonPlanTemplate entity)
        {
            var template = await Context.LessonPlanTemplates.FindAsync(entity.Id);

            template.Name = entity.Name;
            template.PlanTemplate = entity.PlanTemplate;
        }
    }
}
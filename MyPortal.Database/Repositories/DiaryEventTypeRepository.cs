using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class DiaryEventTypeRepository : BaseReadWriteRepository<DiaryEventType>, IDiaryEventTypeRepository
    {
        public DiaryEventTypeRepository(ApplicationDbContext context) : base(context, "DiaryEventType")
        {
        }

        public async Task<IEnumerable<DiaryEventType>> GetAll(bool includeReserved)
        {
            var query = GenerateQuery();

            if (!includeReserved)
            {
                query.Where("DiaryEventType.Reserved", false);
            }

            return await ExecuteQuery(query);
        }
    }
}
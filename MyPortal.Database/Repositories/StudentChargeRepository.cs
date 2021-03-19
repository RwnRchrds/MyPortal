using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class StudentChargeRepository : BaseReadWriteRepository<StudentCharge>, IStudentChargeRepository
    {
        public StudentChargeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "SC")
        {
        }

        public async Task<IEnumerable<StudentCharge>> GetOutstanding()
        {
            throw new NotImplementedException();
        }
    }
}

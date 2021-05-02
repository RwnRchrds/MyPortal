using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class StudentChargeRepository : BaseReadWriteRepository<StudentCharge>, IStudentChargeRepository
    {
        public StudentChargeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task<IEnumerable<StudentCharge>> GetOutstanding()
        {
            throw new NotImplementedException();
        }

        public async Task Update(StudentCharge entity)
        {
            var charge = await Context.StudentCharges.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (charge == null)
            {
                throw new EntityNotFoundException("Student charge not found.");
            }

            charge.Recurrences = entity.Recurrences;
            charge.StartDate = entity.StartDate;
            charge.Description = entity.Description;
        }
    }
}

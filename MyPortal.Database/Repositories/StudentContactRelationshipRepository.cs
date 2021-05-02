using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class StudentContactRelationshipRepository : BaseReadWriteRepository<StudentContactRelationship>, IStudentContactRelationshipRepository
    {
        public StudentContactRelationshipRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        public async Task Update(StudentContactRelationship entity)
        {
            var relationship = await Context.StudentContactRelationships.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (relationship == null)
            {
                throw new EntityNotFoundException("Contact relationship not found.");
            }

            relationship.Correspondence = entity.Correspondence;
            relationship.ParentalResponsibility = entity.ParentalResponsibility;
            relationship.CourtOrder = entity.CourtOrder;
            relationship.PupilReport = entity.PupilReport;
        }
    }
}
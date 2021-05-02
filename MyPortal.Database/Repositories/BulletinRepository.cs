using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class BulletinRepository : BaseReadWriteRepository<Bulletin>, IBulletinRepository
    {
        public BulletinRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
       
        }

        public async Task<IEnumerable<Bulletin>> GetApproved()
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.Approved", "=", true);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<Bulletin>> GetStudent()
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.Approved", "=", true);
            query.Where($"{TblAlias}.ShowStudents", "=", true);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<Bulletin>> GetOwn(Guid authorId)
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.AuthorId", "=", authorId);

            return await ExecuteQuery(query);
        }

        public async Task Update(Bulletin entity)
        {
            var bulletin = await Context.Bulletins.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (bulletin == null)
            {
                throw new EntityNotFoundException("Bulletin not found.");
            }

            bulletin.Title = entity.Title;
            bulletin.Detail = entity.Detail;
            bulletin.ExpireDate = entity.ExpireDate;
            bulletin.Approved = entity.Approved;
            bulletin.StaffOnly = entity.StaffOnly;
        }
    }
}
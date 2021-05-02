using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
    public class GiftedTalentedRepository : BaseReadWriteRepository<GiftedTalented>, IGiftedTalentedRepository
    {
        public GiftedTalentedRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        public async Task<IEnumerable<GiftedTalented>> GetByStudent(Guid studentId)
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.StudentId", studentId);

            return await ExecuteQuery(query);
        }

        public async Task Update(GiftedTalented entity)
        {
            var giftedTalented = await Context.GiftedTalented.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (giftedTalented == null)
            {
                throw new EntityNotFoundException("Gifted talented subject not found.");
            }

            giftedTalented.Notes = entity.Notes;
            giftedTalented.SubjectId = entity.SubjectId;
        }
    }
}
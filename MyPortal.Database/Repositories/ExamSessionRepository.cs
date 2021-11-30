﻿using System.Data.Common;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ExamSessionRepository : BaseReadWriteRepository<ExamSession>, IExamSessionRepository
    {
        public ExamSessionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(ExamSession entity)
        {
            var session = await Context.ExamSessions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (session == null)
            {
                throw new EntityNotFoundException("Exam session not found.");
            }

            session.StartTime = entity.StartTime;
        }
    }
}
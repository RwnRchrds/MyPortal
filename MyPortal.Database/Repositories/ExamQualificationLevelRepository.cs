﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ExamQualificationLevelRepository : BaseReadWriteRepository<ExamQualificationLevel>,
        IExamQualificationLevelRepository
    {
        public ExamQualificationLevelRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("GradeSets as GS", "GS.Id", $"{TblAlias}.DefaultGradeSetId");
            query.LeftJoin("ExamQualifications as EQ", "EQ.Id", $"{TblAlias}.QualificationId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(GradeSet), "GS");
            query.SelectAllColumns(typeof(ExamQualification), "EQ");

            return query;
        }

        protected override async Task<IEnumerable<ExamQualificationLevel>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var qualifications =
                await DbUser.Transaction.Connection
                    .QueryAsync<ExamQualificationLevel, GradeSet, ExamQualification, ExamQualificationLevel>(sql.Sql,
                        (level, gradeSet, qualification) =>
                        {
                            level.DefaultGradeSet = gradeSet;
                            level.Qualification = qualification;

                            return level;
                        }, sql.NamedBindings, DbUser.Transaction);

            return qualifications;
        }

        public async Task Update(ExamQualificationLevel entity)
        {
            var level = await DbUser.Context.ExamQualificationLevels.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (level == null)
            {
                throw new EntityNotFoundException("Qualification level not found.");
            }

            if (level.System)
            {
                throw ExceptionHelper.UpdateSystemEntityException;
            }

            level.JcLevelCode = entity.JcLevelCode;
        }
    }
}
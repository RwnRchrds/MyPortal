using System.Collections.Generic;
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
    public class AchievementRepository : BaseReadWriteRepository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("AchievementTypes as AT", "AT.Id", $"{TblAlias}.AchievementTypeId");
            query.LeftJoin("Locations as L", "L.Id", $"{TblAlias}.LocationId");
            query.LeftJoin("AcademicYears as AY", "AY.Id", $"{TblAlias}.AcademicYearId");
            query.LeftJoin("Users as U", "U.Id", $"{TblAlias}.CreatedById");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AchievementType), "AT");
            query.SelectAllColumns(typeof(Location), "L");
            query.SelectAllColumns(typeof(AcademicYear), "AY");
            query.SelectAllColumns(typeof(User), "U");

            return query;
        }

        protected override async Task<IEnumerable<Achievement>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var achievements = await DbUser.Transaction.Connection
                .QueryAsync<Achievement, AchievementType, Location, AcademicYear, User, Achievement>(
                    sql.Sql,
                    (achievement, type, location, year, user) =>
                    {
                        achievement.Type = type;
                        achievement.Location = location;
                        achievement.AcademicYear = year;
                        achievement.CreatedBy = user;

                        return achievement;
                    }, sql.NamedBindings, DbUser.Transaction);

            return achievements;
        }

        public async Task Update(Achievement entity)
        {
            var achievement = await DbUser.Context.Achievements.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (achievement == null)
            {
                throw new EntityNotFoundException("Achievement not found.");
            }

            achievement.AcademicYearId = entity.AcademicYearId;
            achievement.AchievementTypeId = entity.AchievementTypeId;
            achievement.LocationId = entity.LocationId;
            achievement.CreatedById = entity.CreatedById;
            achievement.Date = entity.Date;
            achievement.Comments = entity.Comments;
            achievement.LocationId = entity.LocationId;
        }
    }
}
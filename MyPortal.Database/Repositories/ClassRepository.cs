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
    public class ClassRepository : BaseReadWriteRepository<Class>, IClassRepository
    {
        public ClassRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        public async Task Update(Class entity)
        {
            var currClass = await Context.Classes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (currClass == null)
            {
                throw new EntityNotFoundException("Class not found.");
            }

            currClass.Code = entity.Code;
            currClass.CourseId = entity.CourseId;
            currClass.CurriculumGroupId = entity.CurriculumGroupId;
        }
    }
}
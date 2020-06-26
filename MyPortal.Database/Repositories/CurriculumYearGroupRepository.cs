﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class CurriculumYearGroupRepository : BaseReadRepository<CurriculumYearGroup>, ICurriculumYearGroupRepository
    {
        public CurriculumYearGroupRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
        }
    }
}
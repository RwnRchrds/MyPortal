﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class SenProvisionTypeRepository : BaseReadRepository<SenProvisionType>, ISenProvisionTypeRepository
    {
        public SenProvisionTypeRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
        }
    }
}
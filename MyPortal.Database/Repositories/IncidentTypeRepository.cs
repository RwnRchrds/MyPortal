﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class IncidentTypeRepository : BaseReadWriteRepository<IncidentType>, IIncidentTypeRepository
    {
        public IncidentTypeRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        }
    }
}
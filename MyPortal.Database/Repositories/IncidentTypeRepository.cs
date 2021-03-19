﻿using System.Data.Common;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class IncidentTypeRepository : BaseReadWriteRepository<IncidentType>, IIncidentTypeRepository
    {
        public IncidentTypeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class RoleRepository : BaseReadWriteRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context, IDbConnection connection) : base(context, connection, "Role")
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Repositories
{
    public class RoleRepository : BaseReadWriteRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context, "Role")
        {

        }
    }
}

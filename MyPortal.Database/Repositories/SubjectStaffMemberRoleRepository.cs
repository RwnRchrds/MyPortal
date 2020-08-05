using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class SubjectStaffMemberRoleRepository : BaseReadWriteRepository<SubjectStaffMemberRole>, ISubjectStaffMemberRoleRepository
    {
        public SubjectStaffMemberRoleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
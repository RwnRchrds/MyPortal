using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class SubjectCodeSetRepository : BaseReadRepository<SubjectCodeSet>, ISubjectCodeSetRepository
    {
        public SubjectCodeSetRepository(IDbConnection connection) : base(connection, "SubjectCodeSet")
        {
        }

        public SubjectCodeSetRepository(ApplicationDbContext context) : base(context, "SubjectCodeSet")
        {
        }
    }
}

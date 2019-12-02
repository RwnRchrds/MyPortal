using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class StudentContactRepository : ReadWriteRepository<StudentContact>, IStudentContactRepository
    {
        public StudentContactRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}
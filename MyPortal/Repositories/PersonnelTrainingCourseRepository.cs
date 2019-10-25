using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class PersonnelTrainingCourseRepository : Repository<PersonnelTrainingCourse>, IPersonnelTrainingCourseRepository
    {
        public PersonnelTrainingCourseRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}
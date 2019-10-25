using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CurriculumStudyTopicRepository : Repository<CurriculumStudyTopic>, ICurriculumStudyTopicRepository
    {
        public CurriculumStudyTopicRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class ProfileCommentRepository : Repository<ProfileComment>, IProfileCommentRepository
    {
        public ProfileCommentRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}
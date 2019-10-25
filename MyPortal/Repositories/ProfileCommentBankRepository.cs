using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class ProfileCommentBankRepository : Repository<ProfileCommentBank>, IProfileCommentBankRepository
    {
        public ProfileCommentBankRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class ProfileCommentBankRepository : ReadWriteRepository<ProfileCommentBank>, IProfileCommentBankRepository
    {
        public ProfileCommentBankRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}
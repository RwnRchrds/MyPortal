using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class SenReviewTypeRepository : ReadRepository<SenReviewType>, ISenReviewTypeRepository
    {
        public SenReviewTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}
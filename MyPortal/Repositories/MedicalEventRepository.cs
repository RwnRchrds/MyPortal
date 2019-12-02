using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class MedicalEventRepository : ReadWriteRepository<MedicalEvent>, IMedicalEventRepository
    {
        public MedicalEventRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}
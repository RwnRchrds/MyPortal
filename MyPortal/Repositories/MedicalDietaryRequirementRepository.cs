using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class MedicalDietaryRequirementRepository : ReadRepository<MedicalDietaryRequirement>, IMedicalDietaryRequirementRepository
    {
        public MedicalDietaryRequirementRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}
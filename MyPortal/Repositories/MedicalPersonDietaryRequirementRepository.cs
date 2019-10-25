using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class MedicalPersonDietaryRequirementRepository : Repository<MedicalPersonDietaryRequirement>, IMedicalPersonDietaryRequirementRepository
    {
        public MedicalPersonDietaryRequirementRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}
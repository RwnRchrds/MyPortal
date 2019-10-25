using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class PersonnelObservationRepository : Repository<PersonnelObservation>, IPersonnelObservationRepository
    {
        public PersonnelObservationRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}
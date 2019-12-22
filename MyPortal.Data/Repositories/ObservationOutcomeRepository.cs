using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class ObservationOutcomeRepository : ReadRepository<ObservationOutcome>, IObservationOutcomeRepository
    {
        public ObservationOutcomeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}

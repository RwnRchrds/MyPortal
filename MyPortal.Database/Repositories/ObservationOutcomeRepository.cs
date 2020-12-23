using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class ObservationOutcomeRepository : BaseReadRepository<ObservationOutcome>, IObservationOutcomeRepository
    {
        public ObservationOutcomeRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}
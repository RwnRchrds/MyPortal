using System.Data;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Repositories
{
    public class BehaviourOutcomeRepository : BaseReadWriteRepository<BehaviourOutcome>, IBehaviourOutcomeRepository
    {
        public BehaviourOutcomeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
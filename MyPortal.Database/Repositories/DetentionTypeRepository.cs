using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Repositories
{
    public class DetentionTypeRepository : BaseReadWriteRepository<DetentionType>, IDetentionTypeRepository
    {
        public DetentionTypeRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
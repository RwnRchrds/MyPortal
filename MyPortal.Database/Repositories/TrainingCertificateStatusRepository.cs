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
    public class TrainingCertificateStatusRepository : BaseReadWriteRepository<TrainingCertificateStatus>, ITrainingCertificateStatusRepository
    {
        public TrainingCertificateStatusRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
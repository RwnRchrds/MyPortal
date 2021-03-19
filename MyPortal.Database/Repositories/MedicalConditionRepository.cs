using System.Data.Common;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class MedicalConditionRepository : BaseReadWriteRepository<MedicalCondition>, IMedicalConditionRepository
    {
        public MedicalConditionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }
    }
}
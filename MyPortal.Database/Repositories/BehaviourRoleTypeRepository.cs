using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories;

public class BehaviourRoleTypeRepository : BaseReadWriteRepository<BehaviourRoleType>, IBehaviourRoleTypeRepository
{
    public BehaviourRoleTypeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
    {
    }

    public async Task Update(BehaviourRoleType entity)
    {
        var roleType = await Context.BehaviourRoleTypes.FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (roleType == null)
        {
            throw new EntityNotFoundException("Role type not found.");
        }

        roleType.DefaultPoints = entity.DefaultPoints;
        roleType.Description = entity.Description;
        roleType.Active = entity.Active;
    }
}
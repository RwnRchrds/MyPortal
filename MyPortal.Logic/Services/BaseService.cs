using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Audit;
using MyPortal.Logic.Models.Data.Settings;


namespace MyPortal.Logic.Services
{
    public abstract class BaseService
    {
        protected readonly ISessionUser User;

        protected BaseService(ISessionUser user) : base()
        {
            User = user;
        }

        protected async Task<IEnumerable<HistoryItem>> GetHistory<T>(IReadRepository<T> repository, Guid entityId) where T : class, IEntity
        {
            await using var unitOfWork = await User.GetConnection();

            var auditLogs = await repository.GetAuditLogsById(entityId);

            var historyItems = new List<HistoryItem>();

            foreach (var auditLog in auditLogs)
            {
                var user = new UserModel(auditLog.User);

                await user.Load(unitOfWork);

                var historyItem = (new HistoryItem
                {
                    Action = auditLog.Action.Description,
                    Date = auditLog.CreatedDate,
                    EntityId = entityId,
                    OldValue = auditLog.OldValue,
                    UserDisplayName = user.GetDisplayName(NameFormat.FullNameAbbreviated)
                });
                
                historyItems.Add(historyItem);
            }
            
            return historyItems;
        }

        protected UnauthorisedException Unauthenticated()
        {
            return new UnauthorisedException("The user is not authenticated.");
        }

        protected void Validate<T>(T model)
        {
            ValidationHelper.ValidateModel(model);
        }
    }
}

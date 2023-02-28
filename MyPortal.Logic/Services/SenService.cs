using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Students;


namespace MyPortal.Logic.Services
{
    public class SenService : BaseUserService, ISenService
    {
        public SenService(ICurrentUser user) : base(user)
        {
        }

        public async Task<IEnumerable<GiftedTalentedModel>> GetGiftedTalentedSubjects(Guid studentId)
        {
            await using var unitOfWork = await DataConnectionFactory.CreateUnitOfWork();
            
            var giftedTalented = await unitOfWork.GiftedTalented.GetByStudent(studentId);

            return giftedTalented.Select(gt => new GiftedTalentedModel(gt));
        }
    }
}

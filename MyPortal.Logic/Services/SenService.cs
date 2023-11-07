using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Students;

namespace MyPortal.Logic.Services
{
    public class SenService : BaseService, ISenService
    {
        public SenService(ISessionUser user) : base(user)
        {
        }

        public async Task<IEnumerable<GiftedTalentedModel>> GetGiftedTalentedSubjects(Guid studentId)
        {
            await using var unitOfWork = await User.GetConnection();

            var giftedTalented = await unitOfWork.GiftedTalented.GetByStudent(studentId);

            return giftedTalented.Select(gt => new GiftedTalentedModel(gt));
        }
    }
}
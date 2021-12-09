using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class SenService : BaseService, ISenService
    {
        public async Task<IEnumerable<GiftedTalentedModel>> GetGiftedTalentedSubjects(Guid studentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var giftedTalented = await unitOfWork.GiftedTalented.GetByStudent(studentId);

                return giftedTalented.Select(gt => new GiftedTalentedModel(gt));
            }
        }

        public SenService(ClaimsPrincipal user) : base(user)
        {
        }
    }
}

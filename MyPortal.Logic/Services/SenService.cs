using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Students;


namespace MyPortal.Logic.Services
{
    public class SenService : BaseService, ISenService
    {
        public async Task<IEnumerable<GiftedTalentedModel>> GetGiftedTalentedSubjects(Guid studentId)
        {
            await using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var giftedTalented = await unitOfWork.GiftedTalented.GetByStudent(studentId);

                return giftedTalented.Select(gt => new GiftedTalentedModel(gt));
            }
        }
    }
}

using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Services
{
    public class DiaryEventService : BaseService, IDiaryEventService
    {
        private readonly IDiaryEventRepository _diaryEventRepository;
        private readonly IDetentionRepository _detentionRepository;
        
        
        public DiaryEventService(ApplicationDbContext context)
        {
            _diaryEventRepository = new DiaryEventRepository(context);
            _detentionRepository = new DetentionRepository(context);
        }

        public override void Dispose()
        {
            _diaryEventRepository.Dispose();
            _detentionRepository.Dispose();
        }
    }
}
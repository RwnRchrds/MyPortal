using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Services
{
    public class DiaryEventService : BaseService, IDiaryEventService
    {
        private readonly IDiaryEventRepository _diaryEventRepository;
        private readonly IDetentionRepository _detentionRepository;
        
        
        public DiaryEventService(IDiaryEventRepository diaryEventRepository) : base("Diary Event")
        {
            _diaryEventRepository = diaryEventRepository;
        }

        public override void Dispose()
        {
            _diaryEventRepository.Dispose();
            _detentionRepository.Dispose();
        }
    }
}
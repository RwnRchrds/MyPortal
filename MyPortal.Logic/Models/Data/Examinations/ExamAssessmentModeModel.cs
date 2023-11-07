using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Examinations
{
    public class ExamAssessmentModeModel : LookupItemModel
    {
        public ExamAssessmentModeModel(ExamAssessmentMode model) : base(model)
        {
            ExternallyAssessed = model.ExternallyAssessed;
        }

        public bool ExternallyAssessed { get; set; }
    }
}
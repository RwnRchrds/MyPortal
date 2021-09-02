using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
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
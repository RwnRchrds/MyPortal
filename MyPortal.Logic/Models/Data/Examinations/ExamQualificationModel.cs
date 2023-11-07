using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Examinations
{
    public class ExamQualificationModel : LookupItemModel
    {
        public ExamQualificationModel(ExamQualification model) : base(model)
        {
            JcQualificationCode = model.JcQualificationCode;
        }

        public string JcQualificationCode { get; set; }
    }
}
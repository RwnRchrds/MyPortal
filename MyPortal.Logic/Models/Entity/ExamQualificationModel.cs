using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
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
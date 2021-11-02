using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class TrainingCertificateStatusModel : LookupItemModel
    {
        public TrainingCertificateStatusModel(TrainingCertificateStatus model) : base(model)
        {
            ColourCode = model.ColourCode;
        }
        
        public string ColourCode { get; set; }
    }
}
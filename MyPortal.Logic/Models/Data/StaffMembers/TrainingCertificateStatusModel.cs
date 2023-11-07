using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.StaffMembers
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
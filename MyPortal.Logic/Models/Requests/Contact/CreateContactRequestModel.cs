using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Requests.Person;

namespace MyPortal.Logic.Models.Requests.Contact
{
    public class CreateContactRequestModel : CreatePersonRequestModel
    {
        public bool ParentalBallot { get; set; }
        
        [StringLength(256)]
        public string PlaceOfWork { get; set; }
        
        [StringLength(256)]
        public string JobTitle { get; set; }
        
        [StringLength(128)]
        public string NiNumber { get; set; }
    }
}
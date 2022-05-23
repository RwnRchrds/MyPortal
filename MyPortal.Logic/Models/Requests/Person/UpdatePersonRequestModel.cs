using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.Person
{
    public class UpdatePersonRequestModel
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        [StringLength(256)]
        public string PreferredFirstName { get; set; }
        
        [StringLength(256)]
        public string PreferredLastName { get; set; }
        
        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }
        
        [StringLength(256)] 
        public string MiddleName { get; set; }
        
        [Required]
        [StringLength(256)]
        public string LastName { get; set; }
        
        [StringLength(10)]
        public string NhsNumber { get; set; }
        
        [Required]
        [StringLength(1)]
        [Gender]
        public string Gender { get; set; }
        
        public DateTime? Dob { get; set; }
        
        public DateTime? Deceased { get; set; }
        
        public Guid? EthnicityId { get; set; }

        public Guid? PhotoId { get; set; }
    }
}
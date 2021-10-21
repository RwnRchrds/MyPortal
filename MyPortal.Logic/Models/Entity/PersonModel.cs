using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class PersonModel : BaseModel
    {
        public PersonModel(Person model) : base(model)
        {
            
        }

        private void LoadFromModel(Person model)
        {
            Id = model.Id;
            DirectoryId = model.DirectoryId;
            Title = model.Title;
            FirstName = model.FirstName;
            MiddleName = model.MiddleName;
            LastName = model.LastName;
            PreferredFirstName = model.PreferredFirstName;
            PreferredLastName = model.PreferredLastName;
            PhotoId = model.PhotoId;
            NhsNumber = model.NhsNumber;
            UpdatedDate = model.UpdatedDate;
            Gender = model.Gender;
            Dob = model.Dob;
            Deceased = model.Deceased;
            EthnicityId = model.EthnicityId;
            Deleted = model.Deleted;

            if (model.Directory != null)
            {
                Directory = new DirectoryModel(model.Directory);
            }
        }
        
        public Guid DirectoryId { get; set; }

        [StringLength(128)]
        public string Title { get; set; }

        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }

        [StringLength(256)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(256)]
        public string LastName { get; set; }
        
        [StringLength(256)] 
        public string PreferredFirstName { get; set; }
        
        [StringLength(256)] 
        public string PreferredLastName { get; set; }
        
        public Guid? PhotoId { get; set; }

        [StringLength(10)]
        public string NhsNumber { get; set; }

        public DateTime UpdatedDate { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        public DateTime? Dob { get; set; }

        public DateTime? Deceased { get; set; }

        public Guid? EthnicityId { get; set; }

        public bool Deleted { get; set; }

        public virtual DirectoryModel Directory { get; set; }

        public virtual PhotoModel Photo { get; set; }

        public virtual EthnicityModel Ethnicity { get; set; }

        public int? Age
        {
            get
            {
                var today = DateTime.Today;
                
                var age = today.Year - Dob?.Year;

                if (age.HasValue)
                {
                    if (Dob.Value.Date > today.AddYears(-age.Value))
                    {
                        age--;
                    }
                }

                return age;
            }
        }

        public string GetName(NameFormat format = NameFormat.Default, bool usePreferred = false, bool includeMiddleName = true)
        {
            string name;

            var firstName = usePreferred
                ? string.IsNullOrWhiteSpace(PreferredFirstName) ? FirstName : PreferredFirstName
                : FirstName;

            var middleName = includeMiddleName ? MiddleName : "";

            var lastName = usePreferred
                ? string.IsNullOrWhiteSpace(PreferredLastName) ? LastName : PreferredLastName
                : LastName;
            
            switch (format)
            {
                case NameFormat.FullName:
                    name = $"{Title} {firstName} {middleName} {lastName}";
                    break;
                case NameFormat.FullNameAbbreviated:
                    name =
                        $"{Title} {firstName.Substring(0, 1)} {middleName?.Substring(0, 1)} {lastName}";
                    break;
                case NameFormat.FullNameNoTitle:
                    name = $"{firstName} {middleName} {lastName}";
                    break;
                case NameFormat.Initials:
                    name =
                        $"{firstName.Substring(0, 1)}{middleName.Substring(0, 1)}{lastName.Substring(0, 1)}";
                    break;
                default:
                    name = $"{lastName}, {firstName} {middleName}";
                    break;
            }

            return name.Replace("  ", " ").Trim();
        }
    }
}
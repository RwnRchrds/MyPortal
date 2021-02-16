using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class PersonModel : BaseModel
    {
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
        public string ChosenFirstName { get; set; }

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

        public string GetDisplayName(NameFormat format = NameFormat.Default, bool useLegalName = true)
        {
            string name;
            switch (format)
            {
                case NameFormat.FullName:
                    name = $"{Title} {(useLegalName ? FirstName : ChosenFirstName)} {MiddleName} {LastName}";
                    break;
                case NameFormat.FullNameAbbreviated:
                    name =
                        $"{Title} {(useLegalName ? FirstName : ChosenFirstName).Substring(0, 1)} {MiddleName?.Substring(0, 1)} {LastName}";
                    break;
                case NameFormat.FullNameNoTitle:
                    name = $"{(useLegalName ? FirstName : ChosenFirstName)} {MiddleName} {LastName}";
                    break;
                case NameFormat.Initials:
                    name =
                        $"{(useLegalName ? FirstName : ChosenFirstName).Substring(0, 1)}{MiddleName.Substring(0, 1)}{LastName.Substring(0, 1)}";
                    break;
                default:
                    name = $"{LastName}, {(useLegalName ? FirstName : ChosenFirstName)} {MiddleName}";
                    break;
            }

            return name.Replace("  ", " ").Trim();
        }
    }
}
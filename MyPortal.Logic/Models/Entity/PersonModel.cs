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
        public PersonModel(Person person)
        {
            Id = person.Id;
            DirectoryId = person.DirectoryId;
            Title = person.Title;
            FirstName = person.FirstName;
            MiddleName = person.MiddleName;
            LastName = person.LastName;
            PhotoId = person.PhotoId;
            NhsNumber = person.NhsNumber;
            UpdatedDate = person.UpdatedDate;
            Gender = person.Gender;
            Dob = person.Dob;
            Deceased = person.Deceased;
            EthnicityId = person.EthnicityId;
            Deleted = person.Deleted;
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
                    name = $"{Title} {FirstName} {MiddleName} {LastName}";
                    break;
                case NameFormat.FullNameAbbreviated:
                    name =
                        $"{Title} {FirstName.Substring(0, 1)} {MiddleName?.Substring(0, 1)} {LastName}";
                    break;
                case NameFormat.FullNameNoTitle:
                    name = $"{FirstName} {MiddleName} {LastName}";
                    break;
                case NameFormat.Initials:
                    name =
                        $"{FirstName.Substring(0, 1)}{MiddleName.Substring(0, 1)}{LastName.Substring(0, 1)}";
                    break;
                default:
                    name = $"{LastName}, {FirstName} {MiddleName}";
                    break;
            }

            return name.Replace("  ", " ").Trim();
        }
    }
}
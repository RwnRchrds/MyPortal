using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class UserModel : BaseModel, ILoadable
    {
        public UserModel(User model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(User model)
        {
            UserName = model.UserName;
            Email = model.Email;
            EmailConfirmed = model.EmailConfirmed;
            PhoneNumber = model.PhoneNumber;
            PhoneNumberConfirmed = model.PhoneNumberConfirmed;
            LockoutEnd = model.LockoutEnd;
            LockoutEnabled = model.LockoutEnabled;
            AccessFailedCount = model.AccessFailedCount;
            CreatedDate = model.CreatedDate;
            PersonId = model.PersonId;
            UserType = model.UserType;
            Enabled = model.Enabled;

            if (model.Person != null)
            {
                Person = new PersonModel(model.Person);
            }
        }
        
        [StringLength(256)]
        public string UserName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public Guid? PersonId { get; set; }
        
        public int UserType { get; set; }
        
        public bool Enabled { get; set; }

        public PersonModel Person { get; set; }

        public string GetDisplayName(NameFormat format = NameFormat.Default, bool usePreferred = false, bool includeMiddleName = true)
        {
            return Person != null ? Person.GetName(format, usePreferred, includeMiddleName) : UserName;
        }

        public async Task<string> GetProfileImageAsBase64(IUnitOfWork unitOfWork)
        {
            if (Person != null)
            {
                await Person.Load(unitOfWork);
            }
            else
            {
                return null;
            }

            return Person.Photo != null ? Convert.ToBase64String(Person.Photo.Data) : null;
        }

        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Users.GetById(Id.Value);
                
                LoadFromModel(model);
            }
        }
    }
}

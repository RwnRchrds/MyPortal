using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using MyPortal.Database;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Contact;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class ContactService : BaseService, IContactService
    {
        public async Task<IEnumerable<StudentModel>> GetReportableStudents(Guid contactId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var students = await unitOfWork.Students.GetByContact(contactId, true);

                return students.Select(s => new StudentModel(s));
            }
        }

        public async Task CreateContact(params CreateContactModel[] models)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var createDate = DateTime.Now;
                
                foreach (var model in models)
                {
                    var contact = new Contact
                    {
                        JobTitle = model.JobTitle,
                        NiNumber = model.NiNumber,
                        PlaceOfWork = model.PlaceOfWork,
                        ParentalBallot = model.ParentalBallot,
                        Person = new Person
                        {
                            FirstName = model.FirstName,
                            PreferredFirstName = model.PreferredFirstName,
                            LastName = model.PreferredLastName,
                            PreferredLastName = model.PreferredLastName,
                            MiddleName = model.MiddleName,
                            Title = model.Title,
                            NhsNumber = model.NhsNumber,
                            CreatedDate = createDate,
                            Deleted = false,
                            Gender = model.Gender,
                            Dob = model.Dob,
                            EthnicityId = model.EthnicityId,
                            Directory = new Directory
                            {
                                Name = "person-root"
                            }
                        }
                    };
                    
                    unitOfWork.Contacts.Create(contact);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}

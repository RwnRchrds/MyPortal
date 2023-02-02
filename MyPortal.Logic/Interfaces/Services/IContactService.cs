using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data.Students;

using MyPortal.Logic.Models.Requests.Contact;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IContactService
    {
        Task<IEnumerable<StudentModel>> GetReportableStudents(Guid contactId);
        Task CreateContact(ContactRequestModel contact);
        Task UpdateContact(Guid contactId, ContactRequestModel contact);
        Task DeleteContact(Guid contactId);
    }
}

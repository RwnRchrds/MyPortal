using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Contact;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IContactService
    {
        Task<IEnumerable<StudentModel>> GetReportableStudents(Guid contactId);
        Task CreateContact(params CreateContactModel[] models);
    }
}

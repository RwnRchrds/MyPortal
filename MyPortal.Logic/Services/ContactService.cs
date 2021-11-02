using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database;
using MyPortal.Database.Models;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

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
    }
}

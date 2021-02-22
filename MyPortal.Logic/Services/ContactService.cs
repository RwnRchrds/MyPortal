using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class ContactService : BaseService, IContactService
    {
        public ContactService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<StudentModel>> GetReportableStudents(Guid contactId)
        {
            var students = await UnitOfWork.Students.GetByContact(contactId, true);

            return students.Select(BusinessMapper.Map<StudentModel>);
        }
    }
}

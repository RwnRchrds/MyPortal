using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class ContactService : BaseService, IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentContactRelationshipRepository _studentContactRelationshipRepository;

        public ContactService(IContactRepository contactRepository, IStudentContactRelationshipRepository studentContactRelationshipRepository, IStudentRepository studentRepository)
        {
            _contactRepository = contactRepository;
            _studentContactRelationshipRepository = studentContactRelationshipRepository;
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<StudentModel>> GetReportableStudents(Guid contactId)
        {
            var students = await _studentRepository.GetByContact(contactId, true);

            return students.Select(BusinessMapper.Map<StudentModel>);
        }

        public override void Dispose()
        {
            _contactRepository.Dispose();
            _studentContactRelationshipRepository.Dispose();
        }
    }
}

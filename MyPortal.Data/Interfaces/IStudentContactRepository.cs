using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IStudentContactRepository : IReadWriteRepository<StudentContact>
    {
        Task<IEnumerable<StudentContact>> GetByStudent(int studentId);
        Task<IEnumerable<StudentContact>> GetByContact(int contactId);
        Task<IEnumerable<StudentContact>> GetByContactParentalResponsibility(int contactId);
    }
}

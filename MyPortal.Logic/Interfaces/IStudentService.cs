using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Details;
using MyPortal.Logic.Models.Student;

namespace MyPortal.Logic.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDetails>> Get(Guid searchType, StudentSearchParams searchParams);

        Lookup GetSearchTypes();
    }
}

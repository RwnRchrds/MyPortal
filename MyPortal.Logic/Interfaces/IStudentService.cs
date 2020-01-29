using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Dtos;

namespace MyPortal.Logic.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAll();
    }
}

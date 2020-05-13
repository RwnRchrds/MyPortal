using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Logic.Interfaces
{
    public interface ISchoolService : IService
    {
        Task<string> GetLocalSchoolName();
    }
}

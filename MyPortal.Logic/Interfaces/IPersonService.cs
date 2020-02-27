using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Lite;

namespace MyPortal.Logic.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonLite>> Search(PersonLite person);
    }
}

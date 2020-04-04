using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Lite;
using MyPortal.Logic.Models.Person;

namespace MyPortal.Logic.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonModel>> Get(PersonSearchParams searchParams);
        Task<PersonModel> GetByUserId(Guid userId);
        Dictionary<string, string> GetGenderOptions();
    }
}

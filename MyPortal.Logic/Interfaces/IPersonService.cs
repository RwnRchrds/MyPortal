using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Requests.Person;

namespace MyPortal.Logic.Interfaces
{
    public interface IPersonService : IService
    {
        Task<IEnumerable<PersonModel>> Get(PersonSearchParams searchParams);
        Task<PersonModel> GetByUserId(Guid userId);
        Dictionary<string, string> GetGenderOptions();
    }
}

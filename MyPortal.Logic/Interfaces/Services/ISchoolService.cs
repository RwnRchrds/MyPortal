using System.Threading.Tasks;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ISchoolService : IService
    {
        Task<string> GetLocalSchoolName();
    }
}

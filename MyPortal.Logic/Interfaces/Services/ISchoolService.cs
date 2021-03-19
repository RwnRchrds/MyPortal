using System.Threading.Tasks;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ISchoolService
    {
        Task<string> GetLocalSchoolName();
    }
}

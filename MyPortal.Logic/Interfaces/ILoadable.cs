using System.Threading.Tasks;
using MyPortal.Database.Interfaces;

namespace MyPortal.Logic.Interfaces
{
    public interface ILoadable
    {
        Task Load(IUnitOfWork unitOfWork);
    }
}
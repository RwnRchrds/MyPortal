using System.Threading.Tasks;
using MyPortal.Database.Interfaces;

namespace MyPortal.Logic.Interfaces
{
    internal interface ILoadable
    {
        Task Load(IUnitOfWork unitOfWork);
    }
}
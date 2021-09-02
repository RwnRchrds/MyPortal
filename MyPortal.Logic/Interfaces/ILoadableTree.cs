using System.Threading.Tasks;
using MyPortal.Database.Interfaces;

namespace MyPortal.Logic.Interfaces
{
    public interface ILoadableTree
    {
        Task Load(IUnitOfWork unitOfWork, bool deep = false);
    }
}
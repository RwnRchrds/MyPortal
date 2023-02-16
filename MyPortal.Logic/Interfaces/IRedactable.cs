using System.Threading.Tasks;
using MyPortal.Database.Interfaces;

namespace MyPortal.Logic.Interfaces;

public interface IRedactable
{
    Task Redact(IUnitOfWork unitOfWork);
}
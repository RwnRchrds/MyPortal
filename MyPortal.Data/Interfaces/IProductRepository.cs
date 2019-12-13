using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IProductRepository : IReadWriteRepository<Product>
    {
        Task<IEnumerable<Product>> GetAvailableByStudent(int studentId);
    }
}

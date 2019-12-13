using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class ProfileLogNoteTypeRepository : ReadRepository<ProfileLogNoteType>, IProfileLogNoteTypeRepository
    {
        public ProfileLogNoteTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}
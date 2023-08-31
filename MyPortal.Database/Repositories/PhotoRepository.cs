using System.Data.Common;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class PhotoRepository : BaseReadWriteRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }
    }
}
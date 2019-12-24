using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class DetentionTypeRepository : ReadWriteRepository<DetentionType>, IDetentionTypeRepository
    {
        public DetentionTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}

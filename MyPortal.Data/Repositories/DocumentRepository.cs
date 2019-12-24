﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class DocumentRepository : ReadWriteRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Document>> GetGeneral()
        {
            return await Context.Documents.Where(x => x.IsGeneral).ToListAsync();
        }

        public async Task<IEnumerable<Document>> GetApproved()
        {
            return await Context.Documents.Where(x => x.IsGeneral && x.Approved).ToListAsync();
        }
    }
}
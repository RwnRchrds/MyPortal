using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class DocumentTypeRepository : ReadRepository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}
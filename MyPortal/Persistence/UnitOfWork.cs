using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Repositories;

namespace MyPortal.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyPortalDbContext _context;

        public UnitOfWork(MyPortalDbContext context)
        {
            _context = context;
            AssessmentResults = new AssessmentResultRepository(_context);
            
        }

        public IAssessmentResultRepository AssessmentResults { get; private set; }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Repository
{
    public class UnitOfWork<DB> : IUnitOfWork<DB>
        where DB:DbContext
    {
        private  DB _context;

        public UnitOfWork(DB context)
        {
            _context = context;
        }


        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (_context == null) return;

            _context.Dispose();
            _context = null;
        }
    }
}

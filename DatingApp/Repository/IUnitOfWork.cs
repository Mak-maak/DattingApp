using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Repository
{
    interface IUnitOfWork<DB> : IDisposable
        where DB : DbContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}

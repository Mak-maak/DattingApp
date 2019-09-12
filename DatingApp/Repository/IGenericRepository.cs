using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Repository
{
   public interface IGenericRepository <T ,DB,U>
        where T:class
        where DB:DbContext
   {
       DB Context();
       void Insert(T entity);
       void Delete(T entity);
       void Update(T entity);

       IEnumerable<T> GetAll();
       IQueryable<T> GetAllAsQueryable();
       T Get(U id);

       Task<IEnumerable<T>> GetAllAsync();
       Task<T> GetAsync(U id);

   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Repository
{
    public class GenericRepository<T , DB , U> : IGenericRepository<T ,DB , U>
        where T : class
        where DB : DbContext
    {

        private readonly DB _dbContext;
        private DbSet<T> entities;

        public DB Context()
        {
            return _dbContext;
        }

        public GenericRepository(DB dbContext)
        {
            _dbContext = dbContext;
            entities = dbContext.Set<T>();
        }

        public void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity");

            entities.Add(entity);
        }

        public void Delete(T entity)
        {
            if(entity == null) throw new ArgumentNullException("Entity");

            entities.Remove(entity);
        }

        public void Update(T entity)
        {
            if(entity == null) throw  new ArgumentNullException("Entity");

            _dbContext.Entry(entity).State = EntityState.Modified;

        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable<T>();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            IQueryable<T> query = entities;
            return query;
        }

        public T Get(U id)
        {
            if(id == null) throw new ArgumentNullException("Entity");

            return entities.Find(id);

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetAsync(U id)
        {
            return await entities.FindAsync(id);
        }

    }
}

using EcisApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IRepository<TEntity>
    {

    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly DataContext db;

        public Repository(DataContext dataContext)
        {
            db = dataContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return db.Set<TEntity>();
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn't retrive entities: {e.Message}");
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await db.AddAsync(entity);
                await db.SaveChangesAsync();
                return entity;
            }
            catch(Exception e)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {e.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                db.Update(entity);
                await db.SaveChangesAsync();

                return entity;
            }
            catch (Exception e)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {e.Message}");
            }
        }
    }
}

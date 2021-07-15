using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        TEntity GetById(object id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(object id);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseModel, new()
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


        public TEntity GetById(object id)
        {
            try
            {
                return db.Set<TEntity>().Find(id);
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
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
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
                entity.UpdatedAt = DateTime.Now;
                db.Update(entity);
                await db.SaveChangesAsync();

                return entity;
            }
            catch (Exception e)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {e.Message}");
            }
        }

        public async Task DeleteAsync(object id)
        {
            var entity = db.Set<TEntity>().Find(id);
            if (entity == null)
            {
                throw new Exception($"{nameof(entity)} not found");
            }
            try
            {
                entity.IsDeleted = true;
                await UpdateAsync(entity);
            }
            catch (Exception)
            {
                throw new Exception($"Cannot delete {nameof(entity)}");
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using NEO_SYSTEM_TECHNOLOGY.Data;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace NEO_SYSTEM_TECHNOLOGY.DAL
{

    public class GenericRepository<TEntity> where TEntity : class
    {
        internal ApplicationDbContext _context;
        internal DbSet<TEntity> _entities;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();            
        }

        public virtual IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _entities;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.AsNoTracking().ToList();


        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] include2)
        {
            IQueryable<TEntity> query = _entities;

            foreach (var property in include2)
            {
                query = query.Include(property);
            }
            query = query.Where(filter);

            return query.AsNoTracking().ToList();
        }

        public virtual TEntity GetByID(object id) 
        {
            return _entities.Find(id);
        }

        public virtual TEntity GetByID(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> entities = _entities;

            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);

            }
            entities = entities.Where(filter);
            return entities.FirstOrDefault();
        }

        public virtual void Insert(TEntity entity)
        {
            _entities.Add(entity);
        }

        public virtual void Delete (object id)
        {
            TEntity entityToDelete = _entities.Find(id);
            Delete(entityToDelete);
            
        }

        public virtual void Delete(TEntity entityToDelete) 
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _entities.Attach(entityToDelete);
            }
            _entities.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _entities.Update(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain.Entities;


namespace Persistance.Repositories
{
    public abstract class Repository<TEntity,T> : IRepository where TEntity : class, IEntity<T>
    {

        protected DbContext _context;

        //public Repository(DbContext Context)
        //{
        //    _context = Context;
        //}

        public virtual TEntity Get(T Id)
        {
            //return _context.Set<TEntity>().Where(x => x.Id == Id).FirstOrDefault();
            return _context.Set<TEntity>().Find(Id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {

            return _context.Set<TEntity>().ToList();

            
        }

        public virtual void Add(TEntity Entity)
        {
            _context.Set<TEntity>().Add(Entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> Entities)
        {
            _context.Set<TEntity>().AddRange(Entities);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> Predicate)
        {
            return _context.Set<TEntity>().Where(Predicate);
        }


        public virtual void Remove(TEntity entity)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                Remove(entity);
            }
        }

        public virtual void Update(TEntity entity)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
        }


        public virtual void Attach(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
        }

        public virtual void Detach(TEntity Entity)
        {
            _context.Entry<TEntity>(Entity).State = EntityState.Detached;
        }

        public void SetContext(DbContext context)
        {
            _context = context;
        }
    }
}

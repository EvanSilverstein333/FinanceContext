using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Domain.Entities;
using System.Data.Entity;

namespace Persistance.Repositories
{

    public interface IRepository
    {
    }

    //public interface IRepository<TEntity,T> where TEntity : IEntity<T>
    //{
    //    TEntity Get(T Id);
    //    IEnumerable<TEntity> GetAll();
    //    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> Predicate);
    //    void Add(TEntity Entity);
    //    void AddRange(IEnumerable<TEntity> Entities);
    //    void Remove(TEntity Entity);
    //    void RemoveRange(IEnumerable<TEntity> Entities);
    //    void Update(TEntity Entity);
    //    void Attach(TEntity Entity);
    //    void Detach(TEntity Entity);
    //    void SetContext(DbContext context);



    //}
}

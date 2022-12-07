using System.Linq.Expressions;
using Core.Entities.Base;

namespace Core.Repositories.Base
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Get(int id);
        IEnumerable<T> GetAll(Expression<Func<T,bool>> predicate=null,
                              Func<IQueryable<T>,IOrderedQueryable<T>> orderBy=null,
                              string includeString=null);
        void Update(T entity);
        void Add(T entity);
        void Remove(T entity);
    }
}

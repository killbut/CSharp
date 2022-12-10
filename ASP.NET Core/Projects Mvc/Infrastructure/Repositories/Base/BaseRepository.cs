using System.Linq.Expressions;
using Core.Entities.Base;
using Core.Repositories.Base;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Repositories.Base
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DataDbContext _context;

        protected BaseRepository(DataDbContext context)
        {
            _context = context;
        }
        
        public T Get(int id)
        {
            return _context.Set<T>().First(x=>x.Id==id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, 
                                    IOrderedQueryable<T>> orderBy = null, 
                                    string includeString=null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (predicate != null)
            {
                query=query.Where(predicate);

            }
            if (!string.IsNullOrEmpty(includeString))
            {
                foreach (var include in includeString.Split())
                {
                    query = query.Include(include);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            if (query.IsNullOrEmpty())
                return new List<T>();
            else
                return query.ToList();
        }
        
        public void Update(T entity)
        {
            _context.Set<T>().Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}

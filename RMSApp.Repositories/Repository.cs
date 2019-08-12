using Microsoft.EntityFrameworkCore;
using RMSApp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMSApp.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        public Repository(DbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            T existing = _context.Set<T>().Find(entity);
            if (existing != null) _context.Set<T>().Remove(existing);
        }

        public IEnumerable<T> Get()
        {
            return _context.Set<T>().AsEnumerable<T>();
        }

        public IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).AsEnumerable<T>();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Attach(entity);
        }
    }
}

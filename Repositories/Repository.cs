using Contracts.Interfaces;
using Entities.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DataContext _db;

        public Repository(DataContext db)
        {
            _db = db;
        }

        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _db.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _db.Set<T>().Where(predicate);
        }

        public T Get(Guid id)
        {
            return _db.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public void Remove(T entity)
        {
            _db.Set<T>().Remove(entity);
        }
        public DataContext DataContext { get { return _db as DataContext; } }
    }
}

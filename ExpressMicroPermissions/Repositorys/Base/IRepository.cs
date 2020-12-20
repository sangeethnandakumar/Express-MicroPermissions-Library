using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExpressMicroPermissions.Repositorys.Base
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        T Get(int Id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    }
}

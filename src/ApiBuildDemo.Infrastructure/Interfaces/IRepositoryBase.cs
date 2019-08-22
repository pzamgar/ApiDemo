using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiBuildDemo.Infrastructure.Interfaces {
    public interface IRepositoryBase<T> {
        Task<T> FindFirst (Expression<Func<T, bool>> expression);
        Task<List<T>> FindAll ();
        Task<List<T>> FindByCondition (Expression<Func<T, bool>> expression);
        Task<T> GetById (Guid id);
        Task<T> Create (T entity);
        Task Delete (T entity);
    }
}
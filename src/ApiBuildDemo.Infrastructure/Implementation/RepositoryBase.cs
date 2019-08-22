using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApiBuildDemo.Infrastructure.Data;
using ApiBuildDemo.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBuildDemo.Infrastructure.Implementation {
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class {
        private readonly RepositoryContext _context;

        public RepositoryBase (RepositoryContext context) {
            _context = context;
        }

        public async Task<T> Create (T entity) {
            await _context.Set<T> ().AddAsync (entity);
            await _context.SaveChangesAsync ();
            return entity;
        }

        public async Task Delete (T entity) {
            _context.Set<T> ().Remove (entity);
            await _context.SaveChangesAsync ();
        }

        public async Task<List<T>> FindAll () {
            return await _context.Set<T> ().ToListAsync ();
        }

        public async Task<List<T>> FindByCondition (Expression<Func<T, bool>> expression) {
            return await _context.Set<T> ().Where (expression).ToListAsync ();
        }

        public async Task<T> FindFirst (Expression<Func<T, bool>> expression) {
            return await _context.Set<T> ().FirstOrDefaultAsync (expression);
        }

        public async Task<T> GetById (Guid id) {
            return await _context.Set<T> ().FindAsync (id);
        }
    }
}
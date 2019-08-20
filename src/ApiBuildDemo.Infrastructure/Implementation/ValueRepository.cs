using System.Threading.Tasks;
using ApiBuildDemo.Infrastructure.Data;
using ApiBuildDemo.Infrastructure.Interfaces;
using ApiBuildDemo.Infrastructure.Models;

namespace ApiBuildDemo.Infrastructure.Implementation {
    public class ValueRepository : RepositoryBase<Value>, IValueRepository {
        public ValueRepository (RepositoryContext context) : base (context) { }

        public Task<Value> GetByTitle(string title)
        {
            throw new System.NotImplementedException();
        }
    }
}
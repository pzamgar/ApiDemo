using System.Threading.Tasks;
using ApiBuildDemo.Infrastructure.Models;

namespace ApiBuildDemo.Infrastructure.Interfaces {
    public interface IValueRepository : IRepositoryBase<Value> {
        Task<Value> GetByTitle (string title);
    }
}
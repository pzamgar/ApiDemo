using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiBuildDemo.Infrastructure.Models;

namespace ApiBuildDemo.Core.Interfases {
    public interface IValueService {
        Task<List<Value>> GetValuesAsync ();
        Task<Value> GetValueByIdAsync (Guid id);
        Task<Value> AddValueAsync (Value value);
        Task DeleteValueById (Guid id);
    }
}
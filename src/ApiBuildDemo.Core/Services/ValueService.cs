using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiBuildDemo.Core.Interfases;
using ApiBuildDemo.Infrastructure.Interfaces;
using ApiBuildDemo.Infrastructure.Models;

namespace ApiBuildDemo.Core.Services {
    public class ValueService : IValueService {
        private readonly ILoggerAdapter<ValueService> _logger;
        private readonly IValueRepository _valueRepository;

        public ValueService (ILoggerAdapter<ValueService> logger, IValueRepository valueRepository) {
            _logger = logger;
            _valueRepository = valueRepository;
        }

        public async Task<List<Value>> GetValuesAsync () {
            return await _valueRepository.FindAll ();
        }

        public async Task<Value> GetValueByIdAsync (Guid id) {
            return await _valueRepository.GetById (id);
        }

        public async Task<Value> AddValueAsync (Value value) {
            value.DateCreated = DateTime.UtcNow;
            value.DateModified = DateTime.UtcNow;
            return await _valueRepository.Create(value);
        }

        public async Task DeleteValueById (Guid id) {
            var value = await _valueRepository.GetById (id);

            if (value == null) {
                return;
            }

            await _valueRepository.Delete(value);
        }
    }
}
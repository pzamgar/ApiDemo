using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBuildDemo.Core.Interfases;
using ApiBuildDemo.Infrastructure.Data;
using ApiBuildDemo.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBuildDemo.Core.Services {
    public class ValueService : IValueService {
        private readonly ILoggerAdapter<ValueService> _logger;
        private readonly ValueContext _context;

        public ValueService (ILoggerAdapter<ValueService> logger, ValueContext context) {
            _logger = logger;
            _context = context;
        }

        public async Task<List<Value>> GetValuesAsync () {
            return await _context.Values.ToListAsync ();
        }

        public async Task<Value> GetValueByIdAsync (int id) {
            return await _context.Values.FindAsync (id);
        }

        public async Task<Value> AddValueAsync (Value value) {
            _context.Values.Add (value);
            await _context.SaveChangesAsync ();
            return value;
        }

        public async Task DeleteValueById (int id) {
            var value = await _context.Values.FindAsync (id);

            if (value == null) {
                return;
            }

            _context.Values.Remove (value);
            await _context.SaveChangesAsync ();
        }
    }
}
using ApiBuildDemo.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ApiBuildDemo.Infrastructure.Data {
    public class ValueContext : DbContext {

        public ValueContext (DbContextOptions<ValueContext> options) : base (options) { }

        public DbSet<Value> Values { get; set; }

    }
}
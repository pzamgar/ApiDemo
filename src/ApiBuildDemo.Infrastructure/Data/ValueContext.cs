using ApiBuildDemo.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBuildDemo.Infrastructure.Data {
    public class ValueContext : DbContext {

        public ValueContext (DbContextOptions<ValueContext> options) : base (options) { }

        public DbSet<Value> Values { get; set; }

        // protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
        //     optionsBuilder.UseSqlServer (
        //         "Server=Localhost,1433;Database=MyDatabase;User Id=SA;Password=P@ssw0rd1*.");
        // }
    }
}
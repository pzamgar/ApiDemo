using ApiBuildDemo.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBuildDemo.Infrastructure.Data {
    public class RepositoryContext : DbContext {

        public RepositoryContext (DbContextOptions<RepositoryContext> options) : base (options) { }

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
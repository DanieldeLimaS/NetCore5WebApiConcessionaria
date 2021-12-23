using Domain.Entities;
using Domain.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infra
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }
        public DbSet<Carros> Carros { get; set; }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Carros>(new CarrosMapping().Configure);
            base.OnModelCreating(builder);
        }

    }
}

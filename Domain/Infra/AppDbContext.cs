#region Manutenção do Código Fonte
/*

<IDENTIFICACAO_DE_MANUTENCAO>
DATA            = "27/12/2021"
PROGRAMADOR     = "Daniel de Lima dos Santos"
MANUTENÇÃO      = "Organização de controle de manutenção de fonte"
</IDENTIFICACAO_DE_MANUTENCAO>
 
 */
#endregion


using Domain.Entities;
using Domain.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infra
{
    public class AppDbContext : DbContext
    {
        public AppDbContext():base()
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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=DBCONCESSIONARIA;uid=sa;pwd=1234;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}

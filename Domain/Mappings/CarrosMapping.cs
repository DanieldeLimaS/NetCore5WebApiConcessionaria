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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Mappings
{
    class CarrosMapping : IEntityTypeConfiguration<Carros>
    {
        public void Configure(EntityTypeBuilder<Carros> builder)
        {
            builder.HasKey(x => x.carId);
            builder.Property(x => x.carMarca).HasColumnType("nvarchar(100)");
            builder.Property(x => x.carModelo).HasColumnType("nvarchar(100)");
            builder.Property(x => x.carAno).HasColumnType("datetime");
            builder.Property(x => x.carPreco).HasColumnType("decimal(10,2)");
            builder.Property(x => x.carDisponivel).HasColumnType("bit");
            builder.Property(x => x.carDataCadastro).HasColumnType("datetime");

        }
    }
}

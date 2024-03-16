using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EntitiesConfiguration
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Paciente");

            builder.HasKey(x=>x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome).IsRequired().HasColumnName("Nome").HasColumnType("VARCHAR").HasMaxLength(50);

            builder.Property(x => x.Sobrenome).IsRequired().HasColumnName("Sobrenome").HasColumnType("VARCHAR").HasMaxLength(50);

            builder.Property(x=>x.Documento).IsRequired().HasColumnName("Documento").HasColumnType("VARCHAR").HasMaxLength(100);

            builder.Property(x => x.DataNascimento).IsRequired().HasColumnName("DataNascimento").HasColumnType("DATE");

            builder.Property(x=>x.Preferencial).IsRequired().HasColumnName("Preferencial").HasColumnType("INT").HasMaxLength(1);

            builder.Property(x=>x.StatusPreferencial).HasColumnName("TipoPreferencial").HasColumnType("INT").HasMaxLength(10);

            builder.Property(x => x.StatusAtendimento).IsRequired().HasColumnName("StatusAtendimento").HasColumnType("INT").HasMaxLength(10);

            builder.Property(x=>x.Datacriação).IsRequired().HasColumnName("Datacriação").HasColumnType("SMALLDATETIME");

        }
    }
}

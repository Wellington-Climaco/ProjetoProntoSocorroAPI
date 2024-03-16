using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EntitiesConfiguration
{
    public class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.ToTable("Funcionario");

            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome).IsRequired().HasColumnName("Nome").HasColumnType("VARCHAR").HasMaxLength(50);

            builder.Property(x => x.Sobrenome).IsRequired().HasColumnName("Sobrenome").HasColumnType("VARCHAR").HasMaxLength(50);

            builder.Property(x=> x.Area).HasColumnName("Area").HasColumnType("INT").HasMaxLength(10);

            builder.Property(x => x.Datacriação).IsRequired().HasColumnName("Datacriação").HasColumnType("DATE");
        }
    }
}

﻿// <auto-generated />
using System;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Funcionario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Area")
                        .HasMaxLength(10)
                        .HasColumnType("INT")
                        .HasColumnName("Area");

                    b.Property<DateTime>("Datacriacao")
                        .HasColumnType("DATETIME")
                        .HasColumnName("Datacriação");

                    b.Property<bool>("EmAtendimento")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Email");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Nome");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Sobrenome");

                    b.HasKey("Id");

                    b.ToTable("Funcionario", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Paciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("DATE")
                        .HasColumnName("DataNascimento");

                    b.Property<DateTime>("Datacriacao")
                        .HasColumnType("SMALLDATETIME")
                        .HasColumnName("Datacriação");

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Documento");

                    b.Property<bool>("EmAtendimento")
                        .HasColumnType("BIT")
                        .HasColumnName("EmAtendimento");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Nome");

                    b.Property<int>("Preferencial")
                        .HasMaxLength(1)
                        .HasColumnType("INT")
                        .HasColumnName("Preferencial");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Sobrenome");

                    b.Property<int>("StatusAtendimento")
                        .HasMaxLength(10)
                        .HasColumnType("INT")
                        .HasColumnName("StatusAtendimento");

                    b.Property<int>("StatusPreferencial")
                        .HasMaxLength(10)
                        .HasColumnType("INT")
                        .HasColumnName("TipoPreferencial");

                    b.HasKey("Id");

                    b.ToTable("Paciente", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Funcionario", b =>
                {
                    b.OwnsOne("Domain.ValueObject.Senha", "Senha", b1 =>
                        {
                            b1.Property<Guid>("FuncionarioId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Hash")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("NVARCHAR")
                                .HasColumnName("Senha");

                            b1.HasKey("FuncionarioId");

                            b1.ToTable("Funcionario");

                            b1.WithOwner()
                                .HasForeignKey("FuncionarioId");
                        });

                    b.Navigation("Senha")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

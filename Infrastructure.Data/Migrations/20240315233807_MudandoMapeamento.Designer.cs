﻿// <auto-generated />
using System;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240315233807_MudandoMapeamento")]
    partial class MudandoMapeamento
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<DateTime>("Datacriação")
                        .HasColumnType("DATE")
                        .HasColumnName("Datacriação");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Nome");

                    b.Property<Guid>("PacienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Sobrenome");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId")
                        .IsUnique();

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

                    b.Property<DateTime>("Datacriação")
                        .HasColumnType("SMALLDATETIME")
                        .HasColumnName("Datacriação");

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Documento");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Nome");

                    b.Property<int>("Preferencial")
                        .HasMaxLength(1)
                        .HasColumnType("INT")
                        .HasColumnName("Preferencial");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR")
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
                    b.HasOne("Domain.Entities.Paciente", "Paciente")
                        .WithOne("Funcionario")
                        .HasForeignKey("Domain.Entities.Funcionario", "PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Domain.Entities.Paciente", b =>
                {
                    b.Navigation("Funcionario")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
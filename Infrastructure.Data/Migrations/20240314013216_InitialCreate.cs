using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Sobrenome = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Area = table.Column<int>(type: "INT", maxLength: 10, nullable: false),
                    Datacriação = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Sobrenome = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Documento = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "DATE", nullable: false),
                    Preferencial = table.Column<bool>(type: "BIT", nullable: false),
                    TipoPreferencial = table.Column<int>(type: "INT", maxLength: 10, nullable: false),
                    StatusAtendimento = table.Column<int>(type: "INT", maxLength: 10, nullable: false),
                    Datacriação = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Paciente");
        }
    }
}

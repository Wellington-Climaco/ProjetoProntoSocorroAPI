using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoPropriedade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Funcionario_FuncionarioId",
                table: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_Paciente_FuncionarioId",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "FuncionarioId",
                table: "Paciente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FuncionarioId",
                table: "Paciente",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_FuncionarioId",
                table: "Paciente",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Funcionario_FuncionarioId",
                table: "Paciente",
                column: "FuncionarioId",
                principalTable: "Funcionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MudandoMapeamentoV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionario_Paciente_PacienteId",
                table: "Funcionario");

            migrationBuilder.DropIndex(
                name: "IX_Funcionario_PacienteId",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Funcionario");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "PacienteId",
                table: "Funcionario",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_PacienteId",
                table: "Funcionario",
                column: "PacienteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_Paciente_PacienteId",
                table: "Funcionario",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

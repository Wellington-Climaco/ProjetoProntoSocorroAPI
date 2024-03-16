using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MudandoMapeamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Preferencial",
                table: "Paciente",
                type: "INT",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "BIT");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<bool>(
                name: "Preferencial",
                table: "Paciente",
                type: "BIT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldMaxLength: 1);
        }
    }
}

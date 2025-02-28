using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adicionadoatabeladeusuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Categoria_Id",
                table: "Despesas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Usuario_Id",
                table: "Despesas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Valor",
                table: "Despesas",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_Categoria_Id",
                table: "Despesas",
                column: "Categoria_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_Usuario_Id",
                table: "Despesas",
                column: "Usuario_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Categorias_Categoria_Id",
                table: "Despesas",
                column: "Categoria_Id",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Usuario_Usuario_Id",
                table: "Despesas",
                column: "Usuario_Id",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Categorias_Categoria_Id",
                table: "Despesas");

            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Usuario_Usuario_Id",
                table: "Despesas");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_Categoria_Id",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_Usuario_Id",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "Categoria_Id",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "Usuario_Id",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Despesas");
        }
    }
}

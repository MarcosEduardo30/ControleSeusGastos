using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTableCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Categorias_Categoria_Id",
                table: "Despesas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_Categoria_Id",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "Categoria_Id",
                table: "Despesas");

            migrationBuilder.AddColumn<int>(
                name: "Categoria",
                table: "Despesas",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Despesas");

            migrationBuilder.AddColumn<int>(
                name: "Categoria_Id",
                table: "Despesas",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_Categoria_Id",
                table: "Despesas",
                column: "Categoria_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Categorias_Categoria_Id",
                table: "Despesas",
                column: "Categoria_Id",
                principalTable: "Categorias",
                principalColumn: "Id");
        }
    }
}

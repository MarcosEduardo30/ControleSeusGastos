using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Correcaonousuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Usuario_Usuario_Id",
                table: "Despesas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Usuarios_Usuario_Id",
                table: "Despesas",
                column: "Usuario_Id",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Usuarios_Usuario_Id",
                table: "Despesas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Usuario_Usuario_Id",
                table: "Despesas",
                column: "Usuario_Id",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

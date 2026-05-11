using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeusPedidos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameFormaDePagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FormasDePagamentos",
                table: "FormasDePagamentos");

            migrationBuilder.RenameTable(
                name: "FormasDePagamentos",
                newName: "FormasDePagamento");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormasDePagamento",
                table: "FormasDePagamento",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FormasDePagamento",
                table: "FormasDePagamento");

            migrationBuilder.RenameTable(
                name: "FormasDePagamento",
                newName: "FormasDePagamentos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormasDePagamentos",
                table: "FormasDePagamentos",
                column: "Id");
        }
    }
}

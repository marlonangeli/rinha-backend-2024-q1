using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RinhaBackend.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Clientes",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Clientes");

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Limite", "SaldoInicial" },
                values: new object[,]
                {
                    { 1, 100000.0, 0.0 },
                    { 2, 80000.0, 0.0 },
                    { 3, 1000000.0, 0.0 },
                    { 4, 10000000.0, 0.0 },
                    { 5, 500000.0, 0.0 }
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DonutsVikets3.Migrations
{
    /// <inheritdoc />
    public partial class newSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, "Donuts tradicionais", "Clássicos" },
                    { 2, "Donuts com vários recheios", "Recheados" },
                    { 3, "Sabores especiais e sazonais", "Especiais" }
                });

            migrationBuilder.InsertData(
                table: "Pedido",
                columns: new[] { "Id", "DataPedido", "Status", "UsuarioId" },
                values: new object[] { 1, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Pendente", 2 });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "CategoriaId", "Descricao", "ImagemUrl", "Nome", "Preco" },
                values: new object[,]
                {
                    { 1, 1, "Donut coberto com glacê", "/img/glazed.jpg", "Glazed", 5.50m },
                    { 2, 1, "Donut com cobertura de chocolate", "/img/chocolate.jpg", "Chocolate", 6.00m },
                    { 3, 2, "Donut recheado com creme", "/img/creme.jpg", "Recheado de Creme", 7.50m },
                    { 4, 2, "Donut recheado com doce de leite", "/img/doceleite.jpg", "Recheado de Doce de Leite", 7.90m },
                    { 5, 3, "Donut especial red velvet", "/img/redvelvet.jpg", "Red Velvet", 8.90m }
                });

            migrationBuilder.InsertData(
                table: "ItemPedido",
                columns: new[] { "Id", "PedidoId", "PrecoUnitario", "ProdutoId", "Quantidade" },
                values: new object[,]
                {
                    { 1, 1, 5.50m, 1, 2 },
                    { 2, 1, 7.50m, 3, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ItemPedido",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ItemPedido",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Produto",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Produto",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Produto",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pedido",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Produto",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Produto",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

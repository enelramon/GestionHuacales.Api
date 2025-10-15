using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GestionHuacales.Api.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntradaHuacales",
                columns: table => new
                {
                    IdEntrada = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreCliente = table.Column<string>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Importe = table.Column<decimal>(type: "TEXT", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    Precio = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradaHuacales", x => x.IdEntrada);
                });

            migrationBuilder.CreateTable(
                name: "TiposHuacales",
                columns: table => new
                {
                    IdTipo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Existencia = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposHuacales", x => x.IdTipo);
                });

            migrationBuilder.CreateTable(
                name: "EntradaHuacalesDetalles",
                columns: table => new
                {
                    IdDetalle = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdEntrada = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTipo = table.Column<int>(type: "INTEGER", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    Precio = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradaHuacalesDetalles", x => x.IdDetalle);
                    table.ForeignKey(
                        name: "FK_EntradaHuacalesDetalles_EntradaHuacales_IdEntrada",
                        column: x => x.IdEntrada,
                        principalTable: "EntradaHuacales",
                        principalColumn: "IdEntrada",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntradaHuacalesDetalles_TiposHuacales_IdTipo",
                        column: x => x.IdTipo,
                        principalTable: "TiposHuacales",
                        principalColumn: "IdTipo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TiposHuacales",
                columns: new[] { "IdTipo", "Descripcion", "Existencia" },
                values: new object[,]
                {
                    { 1, "Verde", 0 },
                    { 2, "Rojo", 0 },
                    { 3, "Verde", 0 },
                    { 4, "Rojo", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntradaHuacalesDetalles_IdEntrada",
                table: "EntradaHuacalesDetalles",
                column: "IdEntrada");

            migrationBuilder.CreateIndex(
                name: "IX_EntradaHuacalesDetalles_IdTipo",
                table: "EntradaHuacalesDetalles",
                column: "IdTipo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntradaHuacalesDetalles");

            migrationBuilder.DropTable(
                name: "EntradaHuacales");

            migrationBuilder.DropTable(
                name: "TiposHuacales");
        }
    }
}

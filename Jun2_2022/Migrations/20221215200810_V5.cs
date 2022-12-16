using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jun22022.Migrations
{
    /// <inheritdoc />
    public partial class V5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kompanija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProsecnaZarada = table.Column<int>(type: "int", nullable: false),
                    BrojDanaZaIsporuku = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kompanija", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roba",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Zapremina = table.Column<int>(type: "int", nullable: false),
                    Tezina = table.Column<int>(type: "int", nullable: false),
                    datumPrijema = table.Column<DateTime>(type: "datetime2", nullable: false),
                    datumIsporuke = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CenaOd = table.Column<int>(type: "int", nullable: false),
                    CenaDo = table.Column<int>(type: "int", nullable: false),
                    KompanijaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roba", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Roba_Kompanija_KompanijaID",
                        column: x => x.KompanijaID,
                        principalTable: "Kompanija",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Vozilo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    KompanijaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozilo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vozilo_Kompanija_KompanijaID",
                        column: x => x.KompanijaID,
                        principalTable: "Kompanija",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roba_KompanijaID",
                table: "Roba",
                column: "KompanijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Vozilo_KompanijaID",
                table: "Vozilo",
                column: "KompanijaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roba");

            migrationBuilder.DropTable(
                name: "Vozilo");

            migrationBuilder.DropTable(
                name: "Kompanija");
        }
    }
}

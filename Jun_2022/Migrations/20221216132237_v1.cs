using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jun2022.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Automobili",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URLSlike = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    DatumProdaje = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobili", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Marke",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marke", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Modeli",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarkaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modeli", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Modeli_Marke_MarkaID",
                        column: x => x.MarkaID,
                        principalTable: "Marke",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Boje",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boje", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Boje_Modeli_ModelID",
                        column: x => x.ModelID,
                        principalTable: "Modeli",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boje_ModelID",
                table: "Boje",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Modeli_MarkaID",
                table: "Modeli",
                column: "MarkaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Automobili");

            migrationBuilder.DropTable(
                name: "Boje");

            migrationBuilder.DropTable(
                name: "Modeli");

            migrationBuilder.DropTable(
                name: "Marke");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Novafascikla4.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aerodromi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lokacija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KapacitetLetelica = table.Column<int>(type: "int", nullable: false),
                    KapacitetPutnika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aerodromi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Letelice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KapacitetPutnika = table.Column<int>(type: "int", nullable: false),
                    Posada = table.Column<int>(type: "int", nullable: false),
                    BrMotora = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Letelice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Letovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojPutnika = table.Column<int>(type: "int", nullable: false),
                    VremePoletanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VremeSletanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LetelicaID = table.Column<int>(type: "int", nullable: true),
                    TackaAID = table.Column<int>(type: "int", nullable: true),
                    TackaBID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Letovi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Letovi_Aerodromi_TackaAID",
                        column: x => x.TackaAID,
                        principalTable: "Aerodromi",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Letovi_Aerodromi_TackaBID",
                        column: x => x.TackaBID,
                        principalTable: "Aerodromi",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Letovi_Letelice_LetelicaID",
                        column: x => x.LetelicaID,
                        principalTable: "Letelice",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Letovi_LetelicaID",
                table: "Letovi",
                column: "LetelicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Letovi_TackaAID",
                table: "Letovi",
                column: "TackaAID");

            migrationBuilder.CreateIndex(
                name: "IX_Letovi_TackaBID",
                table: "Letovi",
                column: "TackaBID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Letovi");

            migrationBuilder.DropTable(
                name: "Aerodromi");

            migrationBuilder.DropTable(
                name: "Letelice");
        }
    }
}

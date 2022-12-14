using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Septembar2022.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cvece",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Izgled = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cvece", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Listovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Izgled = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listovi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Podrucja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Podrucja", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Stabla",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Izgled = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stabla", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Biljke",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kolicina = table.Column<int>(type: "int", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CvetID = table.Column<int>(type: "int", nullable: true),
                    PodrucjeID = table.Column<int>(type: "int", nullable: true),
                    ListID = table.Column<int>(type: "int", nullable: true),
                    StabloID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biljke", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Biljke_Cvece_CvetID",
                        column: x => x.CvetID,
                        principalTable: "Cvece",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Biljke_Listovi_ListID",
                        column: x => x.ListID,
                        principalTable: "Listovi",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Biljke_Podrucja_PodrucjeID",
                        column: x => x.PodrucjeID,
                        principalTable: "Podrucja",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Biljke_Stabla_StabloID",
                        column: x => x.StabloID,
                        principalTable: "Stabla",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "NoveBiljke",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CvetID = table.Column<int>(type: "int", nullable: true),
                    PodrucjeID = table.Column<int>(type: "int", nullable: true),
                    ListID = table.Column<int>(type: "int", nullable: true),
                    StabloID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoveBiljke", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NoveBiljke_Cvece_CvetID",
                        column: x => x.CvetID,
                        principalTable: "Cvece",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NoveBiljke_Listovi_ListID",
                        column: x => x.ListID,
                        principalTable: "Listovi",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NoveBiljke_Podrucja_PodrucjeID",
                        column: x => x.PodrucjeID,
                        principalTable: "Podrucja",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NoveBiljke_Stabla_StabloID",
                        column: x => x.StabloID,
                        principalTable: "Stabla",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Biljke_CvetID",
                table: "Biljke",
                column: "CvetID");

            migrationBuilder.CreateIndex(
                name: "IX_Biljke_ListID",
                table: "Biljke",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_Biljke_PodrucjeID",
                table: "Biljke",
                column: "PodrucjeID");

            migrationBuilder.CreateIndex(
                name: "IX_Biljke_StabloID",
                table: "Biljke",
                column: "StabloID");

            migrationBuilder.CreateIndex(
                name: "IX_NoveBiljke_CvetID",
                table: "NoveBiljke",
                column: "CvetID");

            migrationBuilder.CreateIndex(
                name: "IX_NoveBiljke_ListID",
                table: "NoveBiljke",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_NoveBiljke_PodrucjeID",
                table: "NoveBiljke",
                column: "PodrucjeID");

            migrationBuilder.CreateIndex(
                name: "IX_NoveBiljke_StabloID",
                table: "NoveBiljke",
                column: "StabloID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biljke");

            migrationBuilder.DropTable(
                name: "NoveBiljke");

            migrationBuilder.DropTable(
                name: "Cvece");

            migrationBuilder.DropTable(
                name: "Listovi");

            migrationBuilder.DropTable(
                name: "Podrucja");

            migrationBuilder.DropTable(
                name: "Stabla");
        }
    }
}

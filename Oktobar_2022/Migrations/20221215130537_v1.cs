using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oktobar2022.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dimenzije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Visina = table.Column<int>(type: "int", nullable: false),
                    Sirina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimenzije", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Papiri",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Papiri", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ramovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Materijal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DimenzijaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ramovi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ramovi_Dimenzije_DimenzijaID",
                        column: x => x.DimenzijaID,
                        principalTable: "Dimenzije",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Fotografije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PapirID = table.Column<int>(type: "int", nullable: true),
                    DimenzijaID = table.Column<int>(type: "int", nullable: true),
                    RamID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotografije", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Fotografije_Dimenzije_DimenzijaID",
                        column: x => x.DimenzijaID,
                        principalTable: "Dimenzije",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Fotografije_Papiri_PapirID",
                        column: x => x.PapirID,
                        principalTable: "Papiri",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Fotografije_Ramovi_RamID",
                        column: x => x.RamID,
                        principalTable: "Ramovi",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fotografije_DimenzijaID",
                table: "Fotografije",
                column: "DimenzijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Fotografije_PapirID",
                table: "Fotografije",
                column: "PapirID");

            migrationBuilder.CreateIndex(
                name: "IX_Fotografije_RamID",
                table: "Fotografije",
                column: "RamID");

            migrationBuilder.CreateIndex(
                name: "IX_Ramovi_DimenzijaID",
                table: "Ramovi",
                column: "DimenzijaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fotografije");

            migrationBuilder.DropTable(
                name: "Papiri");

            migrationBuilder.DropTable(
                name: "Ramovi");

            migrationBuilder.DropTable(
                name: "Dimenzije");
        }
    }
}

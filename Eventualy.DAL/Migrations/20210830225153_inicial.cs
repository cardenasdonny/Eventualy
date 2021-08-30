using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventualy.DAL.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TiposDocumento",
                columns: table => new
                {
                    TipoDocumentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDocumento", x => x.TipoDocumentoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombres = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Documento = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TipoDocumentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                    table.ForeignKey(
                        name: "FK_Clientes_TiposDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TiposDocumento",
                        principalColumn: "TipoDocumentoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "TiposDocumento",
                columns: new[] { "TipoDocumentoId", "Nombre" },
                values: new object[] { 1, "CC" });

            migrationBuilder.InsertData(
                table: "TiposDocumento",
                columns: new[] { "TipoDocumentoId", "Nombre" },
                values: new object[] { 2, "TI" });

            migrationBuilder.InsertData(
                table: "TiposDocumento",
                columns: new[] { "TipoDocumentoId", "Nombre" },
                values: new object[] { 3, "CE" });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "ClienteId", "Documento", "Email", "Estado", "Nombres", "TipoDocumentoId" },
                values: new object[] { 1, "72284820", "cardenasdonny@gmail.com", true, "Dony Cardenas", 1 });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "ClienteId", "Documento", "Email", "Estado", "Nombres", "TipoDocumentoId" },
                values: new object[] { 2, "77889966", "sarasofi@gmail.com", true, "Sara Sofia", 2 });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "ClienteId", "Documento", "Email", "Estado", "Nombres", "TipoDocumentoId" },
                values: new object[] { 3, "998822", "samuel@gmail.com", true, "Samuel", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TipoDocumentoId",
                table: "Clientes",
                column: "TipoDocumentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "TiposDocumento");
        }
    }
}

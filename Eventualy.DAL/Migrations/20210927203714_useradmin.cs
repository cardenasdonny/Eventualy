using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventualy.DAL.Migrations
{
    public partial class useradmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Estado", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0h174cfb–4418–1c3e-a2bf-89f716w72cu3", 0, "ad1d0241-a554-4a99-8b1e-7b93bd1a6afa", "Usuario", "cardenasdonny@gmail.com", false, true, false, null, "CARDENASDONNY@GMAIL.COM", "CARDENASDONNY@GMAIL.COM", "AQAAAAEAACcQAAAAENZpyntW1q8uy/0CPm2fFqk3F9yHTZdJXH5jixUcIBQnG3QbjtBmBVF58kzB0XmS9A==", null, false, "d3ae1d77-2252-4ad5-bcf8-12ba7a82d1d6", false, "cardenasdonny@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0h174cfb–4418–1c3e-a2bf-89f716w72cu3");
        }
    }
}

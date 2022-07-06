using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodRaid.InitDB.Migrations
{
    public partial class alterColBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Books",
                newName: "GetBookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GetBookId",
                table: "Books",
                newName: "ClientId");
        }
    }
}

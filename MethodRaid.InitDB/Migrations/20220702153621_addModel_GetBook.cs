using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodRaid.InitDB.Migrations
{
    public partial class addModel_GetBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GetBooks",
                columns: table => new
                {
                    GetBookId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateRel = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateRet = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetBooks", x => x.GetBookId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetBooks");
        }
    }
}

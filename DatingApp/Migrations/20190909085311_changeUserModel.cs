using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.Migrations
{
    public partial class changeUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Password",
                table: "Users",
                nullable: true);
        }
    }
}

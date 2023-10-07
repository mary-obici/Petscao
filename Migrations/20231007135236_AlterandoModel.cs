using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petscao.Migrations
{
    public partial class AlterandoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Timeline",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Timeline");
        }
    }
}

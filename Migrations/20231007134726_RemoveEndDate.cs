using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petscao.Migrations
{
    public partial class RemoveEndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Timeline");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Timeline",
                type: "TEXT",
                nullable: true);
        }
    }
}

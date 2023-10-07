using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petscao.Migrations
{
    public partial class AlterandoModelTimeline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Timeline",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Timeline");
        }
    }
}

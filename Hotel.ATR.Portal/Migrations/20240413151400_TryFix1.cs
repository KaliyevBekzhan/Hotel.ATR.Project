﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.ATR.Portal.Migrations
{
    public partial class TryFix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Field",
                table: "RoomData");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Field",
                table: "RoomData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

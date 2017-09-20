using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HomeMyDay.Migrations
{
    public partial class AccommodationDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Beds",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "Room",
                table: "Holidays");

            migrationBuilder.AddColumn<int>(
                name: "Beds",
                table: "Accommodations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rooms",
                table: "Accommodations",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Beds",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "Rooms",
                table: "Accommodations");

            migrationBuilder.AddColumn<int>(
                name: "Beds",
                table: "Holidays",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Room",
                table: "Holidays",
                nullable: true);
        }
    }
}

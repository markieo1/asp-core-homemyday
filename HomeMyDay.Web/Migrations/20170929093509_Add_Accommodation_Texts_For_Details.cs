using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HomeMyDay.Web.Migrations
{
    public partial class Add_Accommodation_Texts_For_Details : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancellationText",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PricesText",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RulesText",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServicesText",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpaceText",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancellationText",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "PricesText",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "RulesText",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "ServicesText",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "SpaceText",
                table: "Accommodations");
        }
    }
}

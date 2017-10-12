using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HomeMyDay.Web.Migrations
{
	public partial class Add_DepartureDate_And_ReturnDate_To_Holiday : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "Bed",
				table: "Holidays",
				newName: "Beds");

			migrationBuilder.AddColumn<DateTime>(
				name: "DepartureDate",
				table: "Holidays",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

			migrationBuilder.AddColumn<DateTime>(
				name: "ReturnDate",
				table: "Holidays",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "Beds",
				table: "Holidays",
				newName: "Bed");

			migrationBuilder.DropColumn(
				name: "DepartureDate",
				table: "Holidays");

			migrationBuilder.DropColumn(
				name: "ReturnDate",
				table: "Holidays");
		}
	}
}

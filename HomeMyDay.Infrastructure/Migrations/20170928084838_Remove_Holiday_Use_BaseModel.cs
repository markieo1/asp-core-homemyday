using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HomeMyDay.Infrastructure.Migrations
{
	public partial class Remove_Holiday_Use_BaseModel : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{

			migrationBuilder.DropTable(
				name: "Holidays");

			migrationBuilder.DropIndex(
				name: "IX_MediaObjects_AccommodationId",
				table: "MediaObjects");

			migrationBuilder.DropIndex(
				name: "IX_Bookings_AccommodationId",
				table: "Bookings");

			migrationBuilder.DropForeignKey(
				name: "FK_MediaObjects_Accommodations_AccommodationId",
				table: "MediaObjects");

			migrationBuilder.DropForeignKey(
				name: "FK_Bookings_Accommodations_AccommodationId",
				table: "Bookings");

			migrationBuilder.AlterColumn<long>(
				name: "AccommodationId",
				table: "MediaObjects",
				type: "bigint",
				nullable: true,
				oldClrType: typeof(int),
				oldNullable: true);

			migrationBuilder.AddColumn<bool>(
				name: "Primary",
				table: "MediaObjects",
				type: "bit",
				nullable: false,
				defaultValue: false);

			migrationBuilder.AlterColumn<long>(
				name: "AccommodationId",
				table: "Bookings",
				type: "bigint",
				nullable: true,
				oldClrType: typeof(int),
				oldNullable: true);

			migrationBuilder.DropPrimaryKey(
				name: "PK_Bookings",
				table: "Bookings");

			migrationBuilder.AlterColumn<long>(
				name: "Id",
				table: "Bookings",
				type: "bigint",
				nullable: false,
				oldClrType: typeof(int))
				.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
				.OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AddPrimaryKey(
				name: "PK_Bookings",
				table: "Bookings",
				column: "Id");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Accommodations",
				table: "Accommodations");

			migrationBuilder.AlterColumn<long>(
				name: "Id",
				table: "Accommodations",
				type: "bigint",
				nullable: false,
				oldClrType: typeof(int))
				.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
				.OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AddPrimaryKey(
				name: "PK_Accommodations",
				table: "Accommodations",
				column: "Id");

			migrationBuilder.AddColumn<decimal>(
				name: "Price",
				table: "Accommodations",
				type: "decimal(18, 2)",
				nullable: false,
				defaultValue: 0m);

			migrationBuilder.AddColumn<bool>(
				name: "Recommended",
				table: "Accommodations",
				type: "bit",
				nullable: false,
				defaultValue: false);

			migrationBuilder.CreateTable(
				name: "DateEntity",
				columns: table => new
				{
					Id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					AccommodationId = table.Column<long>(type: "bigint", nullable: true),
					Date = table.Column<DateTime>(type: "date", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DateEntity", x => x.Id);
					table.ForeignKey(
						name: "FK_DateEntity_Accommodations_AccommodationId",
						column: x => x.AccommodationId,
						principalTable: "Accommodations",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Bookings_AccommodationId",
				table: "Bookings",
				column: "AccommodationId");

			migrationBuilder.CreateIndex(
				name: "IX_MediaObjects_AccommodationId",
				table: "MediaObjects",
				column: "AccommodationId");

			migrationBuilder.AddForeignKey(
				name: "FK_MediaObjects_Accommodations_AccommodationId",
				column: "AccommodationId",
				table: "MediaObjects",
				principalTable: "Accommodations",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Bookings_Accommodations_AccommodationId",
				column: "AccommodationId",
				table: "Bookings",
				principalTable: "Accommodations",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.CreateIndex(
				name: "IX_DateEntity_AccommodationId",
				table: "DateEntity",
				column: "AccommodationId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "DateEntity");

			migrationBuilder.DropColumn(
				name: "Primary",
				table: "MediaObjects");

			migrationBuilder.DropColumn(
				name: "Price",
				table: "Accommodations");

			migrationBuilder.DropColumn(
				name: "Recommended",
				table: "Accommodations");

			migrationBuilder.DropIndex(
				name: "IX_MediaObjects_AccommodationId",
				table: "MediaObjects");

			migrationBuilder.DropForeignKey(
				name: "FK_MediaObjects_Accommodations_AccommodationId",
				table: "MediaObjects");

			migrationBuilder.AlterColumn<int>(
				name: "AccommodationId",
				table: "MediaObjects",
				nullable: true,
				oldClrType: typeof(long),
				oldType: "bigint",
				oldNullable: true);

			migrationBuilder.DropIndex(
				name: "IX_Bookings_AccommodationId",
				table: "Bookings");

			migrationBuilder.DropForeignKey(
				name: "FK_Bookings_Accommodations_AccommodationId",
				table: "Bookings");

			migrationBuilder.AlterColumn<int>(
				name: "AccommodationId",
				table: "Bookings",
				nullable: true,
				oldClrType: typeof(long),
				oldType: "bigint",
				oldNullable: true);

			migrationBuilder.DropPrimaryKey(
				name: "PK_Bookings",
				table: "Bookings");

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "Bookings",
				nullable: false,
				oldClrType: typeof(long),
				oldType: "bigint")
				.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
				.OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AddPrimaryKey(
				name: "PK_Bookings",
				table: "Bookings",
				column: "Id");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Accommodations",
				table: "Accommodations");

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "Accommodations",
				nullable: false,
				oldClrType: typeof(long),
				oldType: "bigint")
				.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
				.OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AddPrimaryKey(
				name: "PK_Accommodations",
				table: "Accommodations",
				column: "Id");

			migrationBuilder.CreateTable(
				name: "Holidays",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					AccommodationId = table.Column<int>(nullable: true),
					Category = table.Column<string>(nullable: true),
					DepartureDate = table.Column<DateTime>(nullable: false),
					Description = table.Column<string>(nullable: true),
					Image = table.Column<string>(nullable: true),
					Price = table.Column<decimal>(nullable: false),
					Recommended = table.Column<bool>(nullable: false),
					ReturnDate = table.Column<DateTime>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Holidays", x => x.Id);
					table.ForeignKey(
						name: "FK_Holidays_Accommodations_AccommodationId",
						column: x => x.AccommodationId,
						principalTable: "Accommodations",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Holidays_AccommodationId",
				table: "Holidays",
				column: "AccommodationId");

			migrationBuilder.CreateIndex(
				name: "IX_Bookings_AccommodationId",
				table: "Bookings",
				column: "AccommodationId");

			migrationBuilder.CreateIndex(
				name: "IX_MediaObjects_AccommodationId",
				table: "MediaObjects",
				column: "AccommodationId");

			migrationBuilder.AddForeignKey(
				name: "FK_MediaObjects_Accommodations_AccommodationId",
				column: "AccommodationId",
				table: "MediaObjects",
				principalTable: "Accommodations",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Bookings_Accommodations_AccommodationId",
				column: "AccommodationId",
				table: "Bookings",
				principalTable: "Accommodations",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

		}
	}
}

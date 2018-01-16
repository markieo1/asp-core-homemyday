using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HomeMyDay.Infrastructure.Migrations
{
    public partial class update_accommodation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Accommodations_AccommodationId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_DateEntity_Accommodations_AccommodationId",
                table: "DateEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaObjects_Accommodations_AccommodationId",
                table: "MediaObjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Accommodations_AccommodationId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Accommodations");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_AccommodationId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_MediaObjects_AccommodationId",
                table: "MediaObjects");

            migrationBuilder.DropIndex(
                name: "IX_DateEntity_AccommodationId",
                table: "DateEntity");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_AccommodationId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "AccommodationId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "AccommodationId",
                table: "MediaObjects");

            migrationBuilder.DropColumn(
                name: "AccommodationId",
                table: "DateEntity");

            migrationBuilder.DropColumn(
                name: "AccommodationId",
                table: "Bookings");

			migrationBuilder.AddColumn<string>(
				name: "AccommodationId",
				table: "Reviews",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "AccommodationId",
				table: "MediaObjects",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "AccommodationId",
				table: "DateEntity",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "AccommodationId",
				table: "Bookings",
				nullable: true);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccommodationId",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AccommodationId",
                table: "MediaObjects",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AccommodationId",
                table: "DateEntity",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AccommodationId",
                table: "Bookings",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Accommodations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Beds = table.Column<int>(nullable: false),
                    CancellationText = table.Column<string>(nullable: true),
                    Continent = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    Latitude = table.Column<decimal>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Longitude = table.Column<decimal>(nullable: false),
                    MaxPersons = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    PricesText = table.Column<string>(nullable: true),
                    Recommended = table.Column<bool>(nullable: false),
                    Rooms = table.Column<int>(nullable: false),
                    RulesText = table.Column<string>(nullable: true),
                    ServicesText = table.Column<string>(nullable: true),
                    SpaceText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AccommodationId",
                table: "Reviews",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaObjects_AccommodationId",
                table: "MediaObjects",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_DateEntity_AccommodationId",
                table: "DateEntity",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AccommodationId",
                table: "Bookings",
                column: "AccommodationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Accommodations_AccommodationId",
                table: "Bookings",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DateEntity_Accommodations_AccommodationId",
                table: "DateEntity",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaObjects_Accommodations_AccommodationId",
                table: "MediaObjects",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Accommodations_AccommodationId",
                table: "Reviews",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

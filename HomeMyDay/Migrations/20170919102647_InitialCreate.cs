using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HomeMyDay.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accommodations",
                columns: table => new
                {
                    AccommodationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodations", x => x.AccommodationID);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    HolidayID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccommodationID = table.Column<int>(type: "int", nullable: true),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NrPersons = table.Column<int>(type: "int", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.HolidayID);
                    table.ForeignKey(
                        name: "FK_Holidays_Accommodations_AccommodationID",
                        column: x => x.AccommodationID,
                        principalTable: "Accommodations",
                        principalColumn: "AccommodationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_AccommodationID",
                table: "Holidays",
                column: "AccommodationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "Accommodations");
        }
    }
}

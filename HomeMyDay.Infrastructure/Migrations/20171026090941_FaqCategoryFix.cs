using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HomeMyDay.Infrastructure.Migrations
{
    public partial class FaqCategoryFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			/*
            migrationBuilder.DropForeignKey(
                name: "FK_FaqQuestions_FaqCategory_CategoryId",
                table: "FaqQuestions");
			*/

            migrationBuilder.AlterColumn<long>(
                name: "CategoryId",
                table: "FaqQuestions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_FaqQuestions_FaqCategory_CategoryId",
                table: "FaqQuestions",
                column: "CategoryId",
                principalTable: "FaqCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaqQuestions_FaqCategory_CategoryId",
                table: "FaqQuestions");

            migrationBuilder.AlterColumn<long>(
                name: "CategoryId",
                table: "FaqQuestions",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FaqQuestions_FaqCategory_CategoryId",
                table: "FaqQuestions",
                column: "CategoryId",
                principalTable: "FaqCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

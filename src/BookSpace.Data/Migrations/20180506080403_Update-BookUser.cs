using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookSpace.Data.Migrations
{
    public partial class UpdateBookUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "BooksUsers",
                nullable: false,
                defaultValue: 3,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Rate",
                table: "BooksUsers",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "BooksUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "Rate",
                table: "BooksUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);
        }
    }
}

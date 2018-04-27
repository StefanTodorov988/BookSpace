using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookSpace.Data.Migrations
{
    public partial class AddedBookState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "BooksUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 27, 17, 21, 22, 989, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 27, 16, 56, 20, 928, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LockOutEndTime",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 27, 17, 21, 22, 994, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 27, 16, 56, 20, 933, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 27, 17, 21, 22, 993, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 27, 16, 56, 20, 933, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BanEndTime",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 27, 17, 21, 22, 994, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 27, 16, 56, 20, 933, DateTimeKind.Local));

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Tags",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "BooksUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "State",
                table: "BooksUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 27, 16, 56, 20, 928, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 27, 17, 21, 22, 989, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LockOutEndTime",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 27, 16, 56, 20, 933, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 27, 17, 21, 22, 994, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 27, 16, 56, 20, 933, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 27, 17, 21, 22, 993, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BanEndTime",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 27, 16, 56, 20, 933, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 27, 17, 21, 22, 994, DateTimeKind.Local));

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "BooksUsers",
                nullable: true,
                defaultValue: false);
        }
    }
}

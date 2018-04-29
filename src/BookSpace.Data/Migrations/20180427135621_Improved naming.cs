using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookSpace.Data.Migrations
{
    public partial class Improvednaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Comments",
                newName: "Content");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 27, 16, 56, 20, 928, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 26, 11, 32, 57, 776, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LockOutEndTime",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 27, 16, 56, 20, 933, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 26, 11, 32, 57, 779, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 27, 16, 56, 20, 933, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 26, 11, 32, 57, 779, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BanEndTime",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 27, 16, 56, 20, 933, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 26, 11, 32, 57, 779, DateTimeKind.Local));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Comments",
                newName: "Value");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 26, 11, 32, 57, 776, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 27, 16, 56, 20, 928, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LockOutEndTime",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 26, 11, 32, 57, 779, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 27, 16, 56, 20, 933, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 26, 11, 32, 57, 779, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 27, 16, 56, 20, 933, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BanEndTime",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 26, 11, 32, 57, 779, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 27, 16, 56, 20, 933, DateTimeKind.Local));
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookSpace.Data.Migrations
{
    public partial class addedtoUser_isAllowed_isAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 29, 18, 20, 10, 757, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 27, 17, 21, 22, 989, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LockOutEndTime",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 29, 18, 20, 10, 759, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 27, 17, 21, 22, 994, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 29, 18, 20, 10, 759, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 27, 17, 21, 22, 993, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BanEndTime",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 29, 18, 20, 10, 759, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 27, 17, 21, 22, 994, DateTimeKind.Local));

            migrationBuilder.AddColumn<bool>(
                name: "isAdmin",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isAllowed",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAdmin",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "isAllowed",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 27, 17, 21, 22, 989, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 29, 18, 20, 10, 757, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LockOutEndTime",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 27, 17, 21, 22, 994, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 29, 18, 20, 10, 759, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 27, 17, 21, 22, 993, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 29, 18, 20, 10, 759, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BanEndTime",
                table: "UserAccessControl",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 27, 17, 21, 22, 994, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 29, 18, 20, 10, 759, DateTimeKind.Local));
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookSpace.Data.Migrations
{
    public partial class RmoveUserAccessControl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccessControl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccessControl",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    BanEndTime = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2018, 4, 29, 18, 20, 10, 759, DateTimeKind.Local)),
                    LastLogin = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2018, 4, 29, 18, 20, 10, 759, DateTimeKind.Local)),
                    LockOutEndTime = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2018, 4, 29, 18, 20, 10, 759, DateTimeKind.Local)),
                    RegistrationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2018, 4, 29, 18, 20, 10, 757, DateTimeKind.Local))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccessControl", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserAccessControl_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}

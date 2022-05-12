using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User_Back_End.Migrations
{
    public partial class UserRoleFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_User_UserID",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_UserID",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "Role",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_UserID",
                table: "Role",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_User_UserID",
                table: "Role",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

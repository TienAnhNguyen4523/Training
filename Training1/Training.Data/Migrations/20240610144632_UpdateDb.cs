using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_ProductRole_ProductRoleId",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_ProductRoleId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "ProductRoleId",
                table: "Role");

            migrationBuilder.AddColumn<string>(
                name: "ListRole",
                table: "ProductRole",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListRole",
                table: "ProductRole");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductRoleId",
                table: "Role",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_ProductRoleId",
                table: "Role",
                column: "ProductRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_ProductRole_ProductRoleId",
                table: "Role",
                column: "ProductRoleId",
                principalTable: "ProductRole",
                principalColumn: "Id");
        }
    }
}

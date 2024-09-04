using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBARBAndEB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserPermission_UserId",
                table: "UserPermission");

            migrationBuilder.DropIndex(
                name: "IX_ProductRole_BookId",
                table: "ProductRole");

            migrationBuilder.DropIndex(
                name: "IX_Book_AuthorId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_CategoryId",
                table: "Book");

            migrationBuilder.CreateTable(
                name: "BorrowAndReturnBook",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FineAmount = table.Column<int>(type: "int", nullable: false),
                    Librarian = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsReturn = table.Column<bool>(type: "bit", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowAndReturnBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowAndReturnBook_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowAndReturnBook_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExportBill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Librarian = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportBill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportBill_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExportBill_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_UserId",
                table: "UserPermission",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductRole_BookId",
                table: "ProductRole",
                column: "BookId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                table: "Book",
                column: "AuthorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_CategoryId",
                table: "Book",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowAndReturnBook_BookId",
                table: "BorrowAndReturnBook",
                column: "BookId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowAndReturnBook_UserId",
                table: "BorrowAndReturnBook",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExportBill_BookId",
                table: "ExportBill",
                column: "BookId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExportBill_UserId",
                table: "ExportBill",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowAndReturnBook");

            migrationBuilder.DropTable(
                name: "ExportBill");

            migrationBuilder.DropIndex(
                name: "IX_UserPermission_UserId",
                table: "UserPermission");

            migrationBuilder.DropIndex(
                name: "IX_ProductRole_BookId",
                table: "ProductRole");

            migrationBuilder.DropIndex(
                name: "IX_Book_AuthorId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_CategoryId",
                table: "Book");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_UserId",
                table: "UserPermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRole_BookId",
                table: "ProductRole",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                table: "Book",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_CategoryId",
                table: "Book",
                column: "CategoryId");
        }
    }
}
